using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestScenarios.Item.Query.GetTestScenarioItem
{
    public class GetTestScenarioItemQueryHandler : IRequestHandler<GetTestScenarioItemQuery, GetTestScenarioItemDto>
    {
        private readonly TestntDbContext context;
        private readonly IMapper mapper;

        public GetTestScenarioItemQueryHandler(TestntDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GetTestScenarioItemDto> Handle(GetTestScenarioItemQuery request, CancellationToken cancellationToken)
        {
            var testCase = await context.TestScenarios
                .Where(t => t.Id == request.Id)
                .Include(t => t.TestCases)
                .Include(t => t.TestTags)
                //.SingleAsync()
                .ProjectTo<GetTestScenarioItemDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
                ;

            return testCase.Single();
        }
    }
}


