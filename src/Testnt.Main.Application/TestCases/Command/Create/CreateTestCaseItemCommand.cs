using MediatR;
using Microsoft.EntityFrameworkCore;
using Testnt.Common.Exceptions;
using Testnt.Main.Domain.Entity;
using Testnt.Main.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Application.TestCases.Command.Item
{
    public class CreateTestCaseItemCommand : IRequest<CreateTestCaseItemDto>
    {
        public CreateTestCaseItemCommand()
        {
            Tags = new List<Guid>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Guid> Tags { get; set; }
        public Guid ProjectId { get; set; }
        public Guid TestScenarioId { get; set; }

        public class CreateTestCaseItemCommandHandler : IRequestHandler<CreateTestCaseItemCommand, CreateTestCaseItemDto>
        {
            private readonly TestntDbContext context;
            public CreateTestCaseItemCommandHandler(TestntDbContext context)
            {
                this.context = context;
            }

            public async Task<CreateTestCaseItemDto> Handle(CreateTestCaseItemCommand request, CancellationToken cancellationToken)
            {
                var testScenario = await context.TestScenarios
                    .Where(p => p.TestProject.Id.Equals(request.ProjectId))
                    .Where(p => p.Id.Equals(request.TestScenarioId))
                    .FirstOrDefaultAsync();

                if (testScenario == null)
                {
                    throw new EntityNotFoundException(nameof(TestScenario), request.TestScenarioId);
                }

                var entity = new TestCase()
                {
                    Name = request.Name,
                    Status = TestOutlineStatus.Active,
                    TestProject = testScenario.TestProject,
                };

                if (request.Tags.Count > 0)
                {
                    var testTageFromDb = await context.TestTags
                        .Where(tt => tt.ProjectId == request.ProjectId)
                        .Where(tt => request.Tags.Any(rt => rt == tt.Id))
                        .ToListAsync();

                }

                testScenario.TestCases.Add(entity);
                context.TestScenarios.Update(testScenario);
                //context.TestCases.Add(entity);
                await context.SaveChangesAsync(cancellationToken);

                return new CreateTestCaseItemDto
                {
                    Id = entity.Id
                };
            }
        }
    }
}
