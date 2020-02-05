using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Testnt.Main.Application.Components.ProjectComponents.Common;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.Components.ProjectComponents.Tags.Command.Create
{
    public class CreateTagItemCommandValidator : ProjectComponentRequestValidator<CreateTagItemCommand>
    {
        private readonly TestntDbContext context;

        public CreateTagItemCommandValidator(TestntDbContext context) : base(context)
        {
            this.context = context;
            RuleFor(v => v.Name)
                .MaximumLength(50)
                .NotEmpty()
                .WithMessage("Tag name is required.")
                .MustAsync((command, _, cancellation) => HaveUniqueName(command))
                .WithMessage("Project name already exists.");
        }

        private async Task<bool> HaveUniqueName(CreateTagItemCommand command)
        {
            var tagNameExistCheck = await context.Tags
                .Where(t => t.ProjectId.Equals(command.ProjectId))
                .Where(p => p.Name.ToLower().Equals(command.Name.ToLower())).ToListAsync();
            return tagNameExistCheck.Count == 0;
        }
    }
}
