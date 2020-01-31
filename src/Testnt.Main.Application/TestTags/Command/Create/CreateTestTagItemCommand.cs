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

namespace Testnt.Main.Application.TestTags.Command.Create
{
    public class CreateTestTagItemCommand : IRequest<CreateTestTagItemDto>
    {
        public string Name { get; set; }
        public Guid ProjectId { get; set; }
        public ICollection<Guid> TestScenarioIds { get; set; }

        public class CreateTestTagItemCommandHandler : IRequestHandler<CreateTestTagItemCommand, CreateTestTagItemDto>
        {
            private readonly TestntDbContext context;
            public CreateTestTagItemCommandHandler(TestntDbContext context)
            {
                this.context = context;
            }
            public async Task<CreateTestTagItemDto> Handle(CreateTestTagItemCommand request, CancellationToken cancellationToken)
            {
                var project = await context.Projects
                    .Where(p => p.Id.Equals(request.ProjectId))
                    .FirstOrDefaultAsync();

                if (project == null)
                {
                    throw new EntityNotFoundException(nameof(Project), request.ProjectId);
                }

                var entity = new Tag() 
                { 
                    Name = request.Name,
                    ProjectId = request.ProjectId
                };

                if (request.TestScenarioIds.Count > 0)
                {
                    var listFromDb = await context.Scenarios
                        .Where(p => p.Project.Id.Equals(request.ProjectId))
                        .Include(t => t.Tags)
                        .Where(r => request.TestScenarioIds.Contains(r.Id))
                        .ToListAsync()
                        ;
                    var list = listFromDb.Select(t => new TagLink
                    {
                        Tag = entity,
                        Scenario = t
                    });
                    entity.TagLinks.AddRange(list);
                }

                //if (request.TestCaseIds.Count > 0)
                //{
                //    var listFromDb = await context.TestCases
                //        .Where(p => p.TestProject.Id.Equals(request.ProjectId))
                //        .Include(t => t.TestTags)
                //        .Where(r => request.TestCaseIds.Contains(r.Id))
                //        .ToListAsync()
                //        ;
                //    var list = listFromDb.Select(t => new TagLink
                //    {
                //        Tag = entity,
                //        Scenario = t
                //    });
                //    entity.TagLinks.AddRange(list);
                //}

                project.Tags.Add(entity);
                context.Projects.Update(project);
                //context.TestCases.Add(entity);
                await context.SaveChangesAsync(cancellationToken);

                return new CreateTestTagItemDto
                {
                    Id = entity.Id
                };
            }
        }
    }

    public class CreateTestTagItemDto
    {
        public Guid Id { get; set; }
    }
}
