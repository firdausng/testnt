using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestCases.Item.Query.GetTestCaseItem
{
    public class GetTestCaseItemQueryHandler : IRequestHandler<GetTestCaseItemQuery, GetTestCaseItemDto>
    {
        private readonly TestntDbContext context;
        private readonly IMapper mapper;

        public GetTestCaseItemQueryHandler(TestntDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GetTestCaseItemDto> Handle(GetTestCaseItemQuery request, CancellationToken cancellationToken)
        {
            var testCase = await context.TestCases
                .Where(t => t.Id == request.Id)
                .Include(t => t.TestSteps)
                .Include(t => t.TestTags)
                //.SingleAsync()
                .ProjectTo<GetTestCaseItemDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
                ;

            return testCase.Single();
        }
    }
}
