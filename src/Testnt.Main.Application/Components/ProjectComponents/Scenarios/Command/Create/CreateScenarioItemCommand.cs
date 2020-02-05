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
                var project = await context.Projects
                    .Where(p => p.Id.Equals(request.ProjectId))
                    .FirstOrDefaultAsync();

                if (project == null)
                {
                    throw new EntityNotFoundException(nameof(Project), request.ProjectId);
                }

                var entity = new Scenario()
                {
                    Name = request.Name,
                    Description = request.Description,
                    Status = TestOutlineStatus.Draft,
                };

                //if (request.TestCaseIds.Count > 0)
                //{
                //    var listOfTestCasesFromDb = await context.TestCases
                //        .Where(p => p.TestProject.Id.Equals(request.ProjectId))
                //        .Where(r => request.TestCaseIds.Contains(r.Id))
                //        .ToListAsync()
                //        ;
                //    entity.TestCases.AddRange(listOfTestCasesFromDb);
                //}

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


                project.Scenarios.Add(entity);
                context.Projects.Update(project);
                //context.TestCases.Add(entity);
                await context.SaveChangesAsync(cancellationToken);

                return new CreateScenarioItemCommandDto
                {
                    Id = entity.Id
                };
            }
        }
    }
}
