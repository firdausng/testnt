using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testnt.Main.Application.Common;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestSessions.Command.Item
{
    public class CreateTestSessionItemCommandValidator : BaseTenantValidator<CreateTestSessionItemCommand>
    {
        private readonly TestntDbContext context;

        public CreateTestSessionItemCommandValidator(TestntDbContext context)
        {
            this.context = context;
            RuleFor(v => v.Name)
                .MaximumLength(50)
                .NotEmpty()
                .WithMessage("Test Session name is required.")
                .MustAsync((name, cancellation) => HaveUniqueName(name))
                .WithMessage("Test Session name already exists.")
                ;

            RuleFor(v => v.ProjectId)
                .NotEmpty()
                .WithName("Project id")
                .NotNull()
                .WithName("Project id")
                .MustAsync((projectId, cancellation) => ProjectExist(projectId))
                .WithMessage("'Project id' is not exist")
                ;

            RuleFor(v => v.Description)
                .MaximumLength(300)
                ;

        }

        private async Task<bool> HaveUniqueName(string sessionName)
        {
            var testSessionNameExistCheck = await context.TestSessions.Where(p => p.Name.ToLower().Equals(sessionName.ToLower())).ToListAsync();
            return testSessionNameExistCheck.Count == 0;
        }

        private async Task<bool> ProjectExist(Guid projectId)
        {
            var project = await context.Projects
                    .Where(p => p.Id.Equals(projectId))
                    .FirstOrDefaultAsync();
            return project != null;
        }
    }
}
