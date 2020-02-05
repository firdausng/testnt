using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Main.Application.Common;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.Components.ProjectComponents.Scenarios.Query.List
{
    public class GetProjectScenarioListQueryHandler : IRequestHandler<GetProjectScenarioListQuery, GetObjectListVm<GetProjectScenarioListDto>>
    {
        private readonly TestntDbContext context;
        private readonly IMapper mapper;

        public GetProjectScenarioListQueryHandler(TestntDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GetObjectListVm<GetProjectScenarioListDto>> Handle(GetProjectScenarioListQuery request, CancellationToken cancellationToken)
        {
            var testScenariosFromDb = await context.Scenarios
                //.Where(t => t.TenantId.Equals(request.TenantId))
                .Where(t => t.Project.Id.Equals(request.ProjectId))
                .ProjectTo<GetProjectScenarioListDto>(mapper.ConfigurationProvider)
                //.OrderBy(t => t.)
                .ToListAsync(cancellationToken);

            var vm = new GetObjectListVm<GetProjectScenarioListDto>
            {
                Data = testScenariosFromDb,
                Count = testScenariosFromDb.Count
            };

            return vm;
        }
    }
}
