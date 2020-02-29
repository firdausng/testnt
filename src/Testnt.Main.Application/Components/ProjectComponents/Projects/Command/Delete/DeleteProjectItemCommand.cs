using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Common.Exceptions;
using Testnt.Main.Domain.Entity;
using Testnt.Main.Domain.Entity.Projects;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.Components.ProjectComponents.Projects.Command.Delete
{
    public class DeleteProjectItemCommand : IRequest
    {
        public Guid Id { get; set; }
        public class DeleteProjectItemCommandHandler : IRequestHandler<DeleteProjectItemCommand>
        {
            private readonly TestntDbContext context;
            public DeleteProjectItemCommandHandler(TestntDbContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(DeleteProjectItemCommand request, CancellationToken cancellationToken)
            {
                var project = await context.Projects
                    .Where(t => t.Id == request.Id)
                    .SingleOrDefaultAsync();

                if (project == null)
                {
                    throw new EntityNotFoundException(nameof(Project), request.Id);
                }

                context.Projects.Remove(project);
                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
