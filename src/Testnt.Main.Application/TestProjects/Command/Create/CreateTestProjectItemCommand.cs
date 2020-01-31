using MediatR;
using Microsoft.EntityFrameworkCore;
using Testnt.Main.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Main.Infrastructure.Data;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Application.TestProjects.Command.Create
{
    public class CreateTestProjectItemCommand :BaseRequest, IRequest<CreateTestProjectItemDto>
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public class CreateProjectItemCommandHandler : IRequestHandler<CreateTestProjectItemCommand, CreateTestProjectItemDto>
        {
            private readonly TestntDbContext context;
            public CreateProjectItemCommandHandler(TestntDbContext context)
            {
                this.context = context;
            }

            public async Task<CreateTestProjectItemDto> Handle(CreateTestProjectItemCommand request, CancellationToken cancellationToken)
            {
                var entity = new TestProject() { 
                    Name = request.Name, 
                    TenantId= request.TenantId ,
                    IsEnabled = request.IsEnabled
                };
                context.Projects.Add(entity);
                await context.SaveChangesAsync(cancellationToken);

                return new CreateTestProjectItemDto
                {
                    Id = entity.Id
                };
            }
        }
    }
}
