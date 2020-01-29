using MediatR;
using Microsoft.EntityFrameworkCore;
using Testnt.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Main.Infrastructure.Data;
using Testnt.Main.Domain.Entity;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Application.TestScenarios.Command.Item
{
    public class CreateTestScenarioItemCommand : BaseRequest, IRequest<CreateTestScenarioItemCommandDto>
    {
        public CreateTestScenarioItemCommand()
        {
            TagIds = new List<Guid>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Guid> TagIds { get; set; }
        public ICollection<Guid> TestCaseIds { get; set; }
        public Guid ProjectId { get; set; }

        public class CreateTestScenarioItemCommandHandler : IRequestHandler<CreateTestScenarioItemCommand, CreateTestScenarioItemCommandDto>
        {
            private readonly TestntDbContext context;
            public CreateTestScenarioItemCommandHandler(TestntDbContext context)
            {
                this.context = context;
            }

            public async Task<CreateTestScenarioItemCommandDto> Handle(CreateTestScenarioItemCommand request, CancellationToken cancellationToken)
            {
                var project = await context.Projects
                    .Where(p => p.Id.Equals(request.ProjectId))
                    .FirstOrDefaultAsync();

                if (project == null)
                {
                    throw new EntityNotFoundException(nameof(TestProject), request.ProjectId);
                }

                var entity = new TestScenario()
                {
                    Name = request.Name,
                    Status = TestOutlineStatus.Active
                };

                if (request.TagIds.Count > 0)
                {
                    var testTageFromDb = await context.TestTags
                        .Where(tt => tt.ProjectId == request.ProjectId)
                        .Where(tt => request.TagIds.Any(rt => rt == tt.Id))
                        .ToListAsync();

                    //if (testTageFromDb.Count != request.Tags.Count)
                    //{
                    //    var notFoundTestTags = request.Tags
                    //        .Where(rt => !(testTageFromDb.Any(tt => rt == tt.Id)))
                    //        .ToList();
                    //    throw new EntityNotFoundException(nameof(TestTag), notFoundTestTags);
                    //}

                    //entity.Tags = tagEntity;
                }

                project.TestScenarios.Add(entity);
                context.Projects.Update(project);
                //context.TestCases.Add(entity);
                await context.SaveChangesAsync(cancellationToken);

                return new CreateTestScenarioItemCommandDto
                {
                    Id = entity.Id
                };
            }
        }
    }
}
