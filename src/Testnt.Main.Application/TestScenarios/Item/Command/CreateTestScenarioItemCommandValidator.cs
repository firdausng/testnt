using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestScenarios.Item.Command
{
    public class CreateTestScenarioItemCommandValidator : AbstractValidator<CreateTestScenarioItemCommand>
    {
        private readonly TestntDbContext context;
        private List<Guid> notFoundTags;

        public CreateTestScenarioItemCommandValidator(TestntDbContext context)
        {
            this.context = context;
            RuleFor(v => v.Name)
                .MaximumLength(150)
                .NotEmpty()
                .WithName("Test scenario name")
                .NotNull()
                .WithName("Test scenario name")
                .MustAsync((command, name, cancellation) => HaveUniqueNameWithinOneProject(command))
                .WithMessage(c => $"Test case name '{c.Name}' is already existed in this project ({c.ProjectId})")
                ;

            RuleFor(v => v.ProjectId)
                .NotEmpty()
                .WithName("Project id")
                .NotNull()
                .WithName("Project id")
                .MustAsync((projectId, cancellation) => ProjectExist(projectId))
                .WithMessage("'Project id' is not exist")
                ;

            RuleFor(v => v.Tags)
                .NotNull()
                .WithMessage("'Tags' cannot be set to null and is optional parameter")
                .MustAsync((command, tagList, cancellation) => TagsExist(command))
                .WithMessage(c => $"Test case tags ({string.Join(", ", notFoundTags.Select(t => t.ToString()))}) are not existed in this project ({c.ProjectId})")
                ;


            RuleFor(v => v.Description)
                .MaximumLength(300)
                ;

        }
        private async Task<bool> HaveUniqueNameWithinOneProject(CreateTestScenarioItemCommand command)
        {
            var testcaseNameExistCheck = await context.Projects
                .Include(p => p.TestScenarios)
                .Where(p => p.Id == command.ProjectId)
                .SelectMany(p => p.TestScenarios)
                .Select(tc => new
                {
                    tc.Name
                })
                .Where(n => n.Name.ToLower().Trim().Equals(command.Name.ToLower().Trim()))
                .ToListAsync();
            return testcaseNameExistCheck.Count == 0;
        }

        private async Task<bool> ProjectExist(Guid projectId)
        {
            var project = await context.Projects
                    .Where(p => p.Id.Equals(projectId))
                    .FirstOrDefaultAsync();
            return project != null;
        }

        private async Task<bool> TagsExist(CreateTestScenarioItemCommand command)
        {
            if (command.Tags == null || command.Tags.Count == 0)
            {
                return true;
            }
            var testTageFromDb = await context.TestTags
                        .Where(tt => tt.ProjectId == command.ProjectId)
                        .Where(tt => command.Tags.Any(rt => rt == tt.Id))
                        .ToListAsync();

            if (testTageFromDb.Count != command.Tags.Count)
            {
                notFoundTags = command.Tags
                    .Where(rt => !(testTageFromDb.Any(tt => rt == tt.Id)))
                    .ToList();
                //throw new EntityNotFoundException(nameof(TestTag), notFoundTestTags);
            }

            return testTageFromDb.Count == command.Tags.Count;
        }
    }
}
