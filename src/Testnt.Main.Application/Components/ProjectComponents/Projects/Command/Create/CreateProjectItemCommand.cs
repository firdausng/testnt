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

namespace Testnt.Main.Application.Components.ProjectComponents.Projects.Command.Create
{
    public class CreateProjectItemCommand : IRequest<CreateProjectItemDto>
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public class CreateProjectItemCommandHandler : IRequestHandler<CreateProjectItemCommand, CreateProjectItemDto>
        {
            private readonly TestntDbContext context;
            public CreateProjectItemCommandHandler(TestntDbContext context)
            {
                this.context = context;
            }

            public async Task<CreateProjectItemDto> Handle(CreateProjectItemCommand request, CancellationToken cancellationToken)
            {
                var entity = new Project() { 
                    Name = request.Name, 
                    IsEnabled = request.IsEnabled
                };
                context.Projects.Add(entity);
                await context.SaveChangesAsync(cancellationToken);

                return new CreateProjectItemDto
                {
                    Id = entity.Id
                };
            }
        }
    }
}
