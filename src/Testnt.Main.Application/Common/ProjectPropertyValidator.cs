using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.Common
{
    public abstract class ProjectPropertyValidator<T> : AbstractValidator<T> where T : ProjectPropertyRequest
    {
        private readonly TestntDbContext context;

        public ProjectPropertyValidator(TestntDbContext context)
        {
            this.context = context;
            RuleFor(v => v.ProjectId)
                .NotEmpty()
                .WithName("Project id")
                .NotNull()
                .WithName("Project id")
                .MustAsync((command, _, cancellation) => ProjectExist(command))
                .WithMessage("'Project id' is not exist");
            
        }

        private async Task<bool> ProjectExist<T>(T command) where T : ProjectPropertyRequest
        {
            var project = await context.Projects
                    .Where(p => p.Id.Equals(command.ProjectId))
                    .FirstOrDefaultAsync();
            return project != null;
        }
    }
}
