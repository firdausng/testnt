using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Main.Application.Common;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestCases.Query.List
{
    public class GetTestCaseListQueryHandler : IRequestHandler<GetTestCaseListQuery, GetObjectListVm<GetTestCaseListDto>>
    {
        private readonly TestntDbContext context;
        private readonly IMapper mapper;

        public GetTestCaseListQueryHandler(TestntDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GetObjectListVm<GetTestCaseListDto>> Handle(GetTestCaseListQuery request, CancellationToken cancellationToken)
        {
            var testCases = await context.TestCases
                .Where(t => t.TestProject.Id == request.ProjectId)
                .ProjectTo<GetTestCaseListDto>(mapper.ConfigurationProvider)
                //.OrderBy(t => t.)
                .ToListAsync(cancellationToken);

            var vm = new GetObjectListVm<GetTestCaseListDto>
            {
                Data = testCases,
                Count = testCases.Count
            };

            //var vm = new GetTestCaseListVm
            //{
            //    Data = testCases,
            //    Count = testCases.Count
            //};

            return vm;
        }
    }
}
