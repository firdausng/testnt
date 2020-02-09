
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.Components.ProjectComponents.Projects.Command.Create
{
    public class CreateTestProjectCommandValidator : AbstractValidator<CreateProjectItemCommand>
    {
        private readonly TestntDbContext context;

        public CreateTestProjectCommandValidator(TestntDbContext context)
        {
            this.context = context;
            RuleFor(v => v.Name)
                .MaximumLength(50)
                .NotEmpty()
                .WithMessage("Project name is required.")
                .MustAsync((name, cancellation) => HaveUniqueName(name))
                .WithMessage("Project name already exists.")
                ;

            RuleFor(v => v.IsEnabled)
                .Must(v => v == false || v == true)
                .WithName("IsEnabled")
                .NotNull()
                .WithName("IsEnabled")
                ;

        }

        private async Task<bool> HaveUniqueName(string projectName)
        {
            var projectNameExistCheck = await context.Projects
                .Where(p => p.Name.ToLower().Equals(projectName.ToLower()))
                .ToListAsync();
            return projectNameExistCheck.Count == 0;
        }
    }
}
