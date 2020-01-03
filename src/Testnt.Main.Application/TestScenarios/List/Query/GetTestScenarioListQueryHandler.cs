using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestScenarios.List.Query
{
    public class GetTestScenarioListQueryHandler : IRequestHandler<GetTestScenarioListQuery, GetTestScenarioListVm>
    {
        private readonly TestntDbContext context;
        private readonly IMapper mapper;

        public GetTestScenarioListQueryHandler(TestntDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GetTestScenarioListVm> Handle(GetTestScenarioListQuery request, CancellationToken cancellationToken)
        {
            var testScenariosFromDb = await context.TestScenarios
                .ProjectTo<GetTestScenarioListDto>(mapper.ConfigurationProvider)
                //.OrderBy(t => t.)
                .ToListAsync(cancellationToken);

            var vm = new GetTestScenarioListVm
            {
                TestScenarios = testScenariosFromDb,
                Count = testScenariosFromDb.Count
            };

            return vm;
        }
    }
}
