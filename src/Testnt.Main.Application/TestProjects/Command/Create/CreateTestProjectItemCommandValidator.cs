
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Testnt.Main.Application.Common;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestProjects.Command.Create
{
    public class CreateTestProjectCommandValidator : AbstractValidator<CreateTestProjectItemCommand>
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
                .NotEmpty()
                .WithName("IsEnabled")
                .NotNull()
                .WithName("IsEnabled")
                ;

            //RuleFor(v => v.TenantId)
            //    .NotEmpty()
            //    .WithMessage("Tenant id is missing.");

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
