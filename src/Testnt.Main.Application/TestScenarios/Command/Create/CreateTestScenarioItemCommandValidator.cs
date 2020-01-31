using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testnt.Main.Application.Common;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestScenarios.Command.Item
{
    public class CreateTestScenarioItemCommandValidator : AbstractValidator<CreateTestScenarioItemCommand>
    {
        private readonly TestntDbContext context;

        public CreateTestScenarioItemCommandValidator(TestntDbContext context)
        {
            this.context = context;
            RuleFor(v => v.Name)
                .MaximumLength(150)
                .NotEmpty()
                .WithName("Test scenario name")
                .NotNull()
                .WithName("Test scenario name")
                .MustAsync((command, _, cancellation) => HaveUniqueNameWithinOneProject(command))
                .WithMessage(c => $"Test case name '{c.Name}' is already existed in this project ({c.ProjectId})")
                ;

            RuleFor(v => v.ProjectId)
                .NotEmpty()
                .WithName("Project id")
                .NotNull()
                .WithName("Project id")
                .MustAsync((command, _, cancellation) => ProjectExist(command))
                .WithMessage("'Project id' is not exist")
                ;

            RuleFor(v => v.TagIds)
                .NotNull()
                .WithMessage("'Tags' cannot be set to null and is optional parameter")
                .MustAsync((command, _, cancellation) => TagsExist(command))
                ;

            RuleFor(v => v.Description)
                .MaximumLength(300)
                ;

        }

        private async Task<bool> HaveUniqueNameWithinOneProject(CreateTestScenarioItemCommand command)
        {
            var testcaseNameExistCheck = await context.Projects
                .Include(p => p.Scenarios)
                .Where(p => p.Id == command.ProjectId)
                .SelectMany(p => p.Scenarios)
                .Select(tc => new
                {
                    tc.Name
                })
                .Where(n => n.Name.ToLower().Trim().Equals(command.Name.ToLower().Trim()))
                .ToListAsync();
            return testcaseNameExistCheck.Count == 0;
        }

        private async Task<bool> ProjectExist(CreateTestScenarioItemCommand command)
        {
            var project = await context.Projects
                    .Where(p => p.Id.Equals(command.ProjectId))
                    .FirstOrDefaultAsync();
            return project != null;
        }

        private async Task<bool> TagsExist(CreateTestScenarioItemCommand command)
        {
            if (command.TagIds == null || command.TagIds.Count == 0)
            {
                return true;
            }
            var testTagsFromDb = await context.Tags
                        .Where(tt => tt.ProjectId == command.ProjectId)
                        .Where(tt => command.TagIds.Any(rt => rt == tt.Id))
                        .ToListAsync();

            return testTagsFromDb.Count == command.TagIds.Count;
        }
    }
}
