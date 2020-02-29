using MediatR;
using Microsoft.EntityFrameworkCore;
using Testnt.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Main.Infrastructure.Data;
using Testnt.Main.Domain.Entity;
using Testnt.Main.Application.Components.ProjectComponents.Common;
using Testnt.Main.Domain.Entity.Projects;

namespace Testnt.Main.Application.Components.ProjectComponents.Scenarios.Command.Item
{
    public class CreateScenarioItemCommand : ProjectComponentRequest, IRequest<CreateScenarioItemCommandDto>
    {
        public CreateScenarioItemCommand()
        {
            TagIds = new List<Guid>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Guid> TagIds { get; set; }

        public class CreateTestScenarioItemCommandHandler : IRequestHandler<CreateScenarioItemCommand, CreateScenarioItemCommandDto>
        {
            private readonly TestntDbContext context;
            public CreateTestScenarioItemCommandHandler(TestntDbContext context)
            {
                this.context = context;
            }

            public async Task<CreateScenarioItemCommandDto> Handle(CreateScenarioItemCommand request, CancellationToken cancellationToken)
            {
                var entity = new Scenario()
                {
                    Name = request.Name,
                    Description = request.Description,
                    Status = TestOutlineStatus.Draft,
                    ProjectId = request.ProjectId
                };

                if (request.TagIds.Count > 0)
                {
                    var listOfTagsFromDb = await context.Tags
                        .Where(p => p.ProjectId.Equals(request.ProjectId))
                        .Include(t => t.TagLinks)
                        .Where(r => request.TagIds.Contains(r.Id))
                        .ToListAsync()
                        ;
                    var list = listOfTagsFromDb.Select(t => new TagLink 
                    { 
                        Tag = t,
                        Scenario = entity
                    });
                    entity.Tags.AddRange(list);
                }


                context.Scenarios.Update(entity);
                await context.SaveChangesAsync(cancellationToken);

                return new CreateScenarioItemCommandDto
                {
                    Id = entity.Id
                };
            }
        }
    }
}
