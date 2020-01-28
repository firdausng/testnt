using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Common.Exceptions;
using Testnt.Main.Application.Common;
using Testnt.Main.Domain.Entity;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestProjects.Item.Command.DeleteTestProjectItem
{
    public class DeleteTestProjectItemCommand : BaseRequest, IRequest
    {
        public DeleteTestProjectItemCommand(Guid tenantId)
        {
            this.TenantId = tenantId;
        }

        public Guid Id { get; set; }
        public class DeleteProjectItemCommandHandler : IRequestHandler<DeleteTestProjectItemCommand>
        {
            private readonly TestntDbContext context;
            public DeleteProjectItemCommandHandler(TestntDbContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(DeleteTestProjectItemCommand request, CancellationToken cancellationToken)
            {
                var project = await context.Projects
                    .Where(t => t.TenantId.Equals(request.TenantId))
                    .Where(t => t.Id == request.Id)
                    .SingleOrDefaultAsync();

                if (project == null)
                {
                    throw new EntityNotFoundException(nameof(TestProject), request.Id);
                }

                context.Projects.Remove(project);
                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

    public class DeleteTestProjectItemDto
    {
    }
}
