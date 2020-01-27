using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestCases.List.Query.GetTestCaseList
{
    public class GetTestCaseListQueryHandler : IRequestHandler<GetTestCaseListQuery, GetTestCaseListVm>
    {
        private readonly TestntDbContext context;
        private readonly IMapper mapper;

        public GetTestCaseListQueryHandler(TestntDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GetTestCaseListVm> Handle(GetTestCaseListQuery request, CancellationToken cancellationToken)
        {
            var testCases = await context.TestCases
                .ProjectTo<GetTestCaseListDto>(mapper.ConfigurationProvider)
                //.OrderBy(t => t.)
                .ToListAsync(cancellationToken);

            var vm = new GetTestCaseListVm
            {
                Data = testCases,
                Count = testCases.Count
            };

            return vm;
        }
    }
}
