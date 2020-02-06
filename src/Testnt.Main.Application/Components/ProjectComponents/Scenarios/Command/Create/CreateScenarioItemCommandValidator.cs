using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Testnt.Main.Application.Components.ProjectComponents.Common;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.Components.ProjectComponents.Scenarios.Command.Item
{
    public class CreateScenarioItemCommandValidator : ProjectComponentRequestValidator<CreateScenarioItemCommand>
    {

        public CreateScenarioItemCommandValidator(TestntDbContext context):base(context)
        {
            RuleFor(v => v.Name)
                .MaximumLength(150)
                .NotEmpty()
                .WithName("Test scenario name")
                .NotNull()
                .WithName("Test scenario name")
                .MustAsync((command, _, cancellation) => HaveUniqueNameWithinOneProject(command))
                .WithMessage(c => $"Test case name '{c.Name}' is already existed in this project ({c.ProjectId})");

            RuleFor(v => v.TagIds)
                .NotNull()
                .WithMessage("'Tags' cannot be set to null and is optional parameter")
                .MustAsync((command, _, cancellation) => TagsExist(command));

            RuleFor(v => v.Description)
                .MaximumLength(300);
        }
        private async Task<bool> HaveUniqueNameWithinOneProject(CreateScenarioItemCommand command)
        {
            var scenarioNameExistCheck = await context.Scenarios
                .Where(p => p.Id.Equals(command.ProjectId))
                .Where(n => n.Name.ToLower().Trim().Equals(command.Name.ToLower().Trim()))
                .SingleOrDefaultAsync();
            return scenarioNameExistCheck != null;
        }

        private async Task<bool> TagsExist(CreateScenarioItemCommand command)
        {
            if (command.TagIds == null || command.TagIds.Count == 0)
            {
                return true;
            }
            var testTagsFromDb = await context.Tags
                        .Where(tt => tt.ProjectId.Equals(command.ProjectId))
                        .Where(tt => command.TagIds.Any(rt => rt.Equals(tt.Id)))
                        .ToListAsync();

            return testTagsFromDb.Count == command.TagIds.Count;
        }
    }
}
