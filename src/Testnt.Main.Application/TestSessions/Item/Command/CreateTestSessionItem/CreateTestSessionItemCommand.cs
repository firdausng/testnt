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
using Testnt.Main.Domain.Entity.TestSessionEntity;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestSessions.Item.Command.CreateTestSessionItem
{
    public class CreateTestSessionItemCommand : BaseRequest, IRequest<CreateTestSessionItemDto>
    {
        public string Name { get; set; }
        public Guid ProjectId { get; set; }
        public string Description { get; set; }
        public class CreateTestSessionItemCommandHandler : IRequestHandler<CreateTestSessionItemCommand, CreateTestSessionItemDto>
        {
            private readonly TestntDbContext context;
            public CreateTestSessionItemCommandHandler(TestntDbContext context)
            {
                this.context = context;
            }

            public async Task<CreateTestSessionItemDto> Handle(CreateTestSessionItemCommand request, CancellationToken cancellationToken)
            {
                var project = await context.Projects
                    .Where(p => p.Id.Equals(request.ProjectId))
                    .FirstOrDefaultAsync();

                if (project == null)
                {
                    throw new EntityNotFoundException(nameof(TestProject), request.ProjectId);
                }

                var entity = new TestSession() { Name = request.Name };
                
                project.TestSessions.Add(entity);
                context.Projects.Update(project);
                //context.TestCases.Add(entity);
                await context.SaveChangesAsync(cancellationToken);

                return new CreateTestSessionItemDto
                {
                    Id = entity.Id
                };
            }
        }
    }
}
