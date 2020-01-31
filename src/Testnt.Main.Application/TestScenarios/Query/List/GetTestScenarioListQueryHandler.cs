using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Main.Application.Common;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestScenarios.Query.List
{
    public class GetTestScenarioListQueryHandler : IRequestHandler<GetTestScenarioListQuery, GetObjectListVm<GetTestScenarioListDto>>
    {
        private readonly TestntDbContext context;
        private readonly IMapper mapper;

        public GetTestScenarioListQueryHandler(TestntDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GetObjectListVm<GetTestScenarioListDto>> Handle(GetTestScenarioListQuery request, CancellationToken cancellationToken)
        {
            var testScenariosFromDb = await context.Scenarios
                //.Where(t => t.TenantId.Equals(request.TenantId))
                .Where(t => t.Project.Id.Equals(request.ProjectId))
                .ProjectTo<GetTestScenarioListDto>(mapper.ConfigurationProvider)
                //.OrderBy(t => t.)
                .ToListAsync(cancellationToken);

            var vm = new GetObjectListVm<GetTestScenarioListDto>
            {
                Data = testScenariosFromDb,
                Count = testScenariosFromDb.Count
            };

            return vm;
        }
    }
}
