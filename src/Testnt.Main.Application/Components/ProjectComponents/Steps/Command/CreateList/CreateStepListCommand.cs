using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Main.Application.Components.ProjectComponents.Common;
using Testnt.Main.Domain.Entity;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.Components.ProjectComponents.Steps.Command.CreateList
{
    public class CreateStepListCommand : ProjectComponentRequest, IRequest<CreateStepListCommandDto>
    {
        public List<StepItem> StepList { get; set; }
        public class CreateStepListCommandHandler : IRequestHandler<CreateStepListCommand, CreateStepListCommandDto>
        {
            private readonly TestntDbContext context;

            public CreateStepListCommandHandler(TestntDbContext context)
            {
                this.context = context;
            }

            public async Task<CreateStepListCommandDto> Handle(CreateStepListCommand request, CancellationToken cancellationToken)
            {
                var entities = request.StepList.Select(s =>
                {
                    var entity = new Step()
                    {
                        Description = s.Description,
                        Order = s.Order,
                        ProjectId = request.ProjectId
                    };
                    return entity;
                });
                
                await context.Steps.AddRangeAsync(entities, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return new CreateStepListCommandDto
                {
                    StepIds = entities.Select(s => s.Id).ToList()
                };
            }
        }
    }

    public class StepItem
    {
        public string Description { get; set; }
        public int Order { get; set; }
    }

    public class CreateStepListCommandDto
    {
        public List<Guid> StepIds { get; set; }
    }

    public class CreateStepListCommandValidator : ProjectComponentRequestValidator<CreateStepListCommand>
    {
        public CreateStepListCommandValidator(TestntDbContext context) : base(context)
        {
            //RuleFor(v => v.Description)
            //    .MaximumLength(300)
            //    .NotEmpty()
            //    .WithName("Description")
            //    .NotNull()
            //    .WithName("Description")
            //    .MustAsync((command, _, cancellation) => HaveUniqueName(command))
            //    .WithMessage(c => $"Test step'{c.Description}' is already existed");

        }

        private async Task<bool> HaveUniqueName(CreateStepListCommand command)
        {
            var testStepExistCheck = await context.Steps
                .Where(p => p.Id.Equals(command.ProjectId))
                //.Where(n => n.Description.ToLower().Trim().Equals(command.Description.ToLower().Trim()))
                .SingleOrDefaultAsync();
            return testStepExistCheck != null;
        }
    }
}
