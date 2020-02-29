using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Main.Application.Components.ProjectComponents.Common;
using Testnt.Main.Domain.Entity;
using Testnt.Main.Domain.Entity.Projects;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.Components.ProjectComponents.Steps.Command.Create
{
    public class CreateStepItemCommand : ProjectComponentRequest, IRequest<CreateStepItemCommandDto>
    {
        public string Description { get; set; }
        public int Order { get; set; }

        public class CreateStepItemCommandHandler : IRequestHandler<CreateStepItemCommand, CreateStepItemCommandDto>
        {
            private readonly TestntDbContext context;

            public CreateStepItemCommandHandler(TestntDbContext context)
            {
                this.context = context;
            }

            public async Task<CreateStepItemCommandDto> Handle(CreateStepItemCommand request, CancellationToken cancellationToken)
            {
                var entity = new Step()
                {
                    Description = request.Description,
                    Order = request.Order,
                    ProjectId = request.ProjectId
                };


                await context.Steps.AddAsync(entity, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return new CreateStepItemCommandDto
                {
                    Id = entity.Id
                };
            }
        }
    }

    public class CreateStepItemCommandDto
    {
        public Guid Id { get; set; }
    }

    public class CreateStepItemCommandValidator : ProjectComponentRequestValidator<CreateStepItemCommand>
    {
        public CreateStepItemCommandValidator(TestntDbContext context) : base(context)
        {
            RuleFor(v => v.Description)
                .MaximumLength(300)
                .NotEmpty()
                .WithName("Description")
                .NotNull()
                .WithName("Description")
                .MustAsync((command, _, cancellation) => HaveUniqueName(command))
                .WithMessage(c => $"Test step'{c.Description}' is already existed");

        }

        private async Task<bool> HaveUniqueName(CreateStepItemCommand command)
        {
            var testStepExistCheck = await context.Steps
                .Where(p => p.Id.Equals(command.ProjectId))
                .Where(n => n.Description.ToLower().Trim().Equals(command.Description.ToLower().Trim()))
                .SingleOrDefaultAsync();
            return testStepExistCheck != null;
        }
    }
}
