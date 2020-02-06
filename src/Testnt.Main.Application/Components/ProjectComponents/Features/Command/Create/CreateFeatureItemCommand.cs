using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Main.Application.Components.ProjectComponents.Common;
using Testnt.Main.Domain.Entity;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.Components.ProjectComponents.Features.Command.Create
{
    public class CreateFeatureItemCommand: ProjectComponentRequest, IRequest<CreateFeatureItemCommandDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public class CreateFeatureItemCommandHandler : IRequestHandler<CreateFeatureItemCommand, CreateFeatureItemCommandDto>
        {
            private readonly TestntDbContext context;

            public CreateFeatureItemCommandHandler(TestntDbContext context)
            {
                this.context = context;
            }

            public async Task<CreateFeatureItemCommandDto> Handle(CreateFeatureItemCommand request, CancellationToken cancellationToken)
            {
                var entity = new Feature()
                {
                    Name = request.Name,
                    Description = request.Description,
                    ProjectId = request.ProjectId
                };


                await context.Features.AddAsync(entity, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return new CreateFeatureItemCommandDto
                {
                    Id = entity.Id
                };
            }
        }
    }

    public class CreateFeatureItemCommandDto
    {
        public Guid Id { get; set; }
    }

    public class CreateFeatureItemCommandValidator : ProjectComponentRequestValidator<CreateFeatureItemCommand>
    {
        public CreateFeatureItemCommandValidator(TestntDbContext context) : base(context)
        {
        }
    }
}
