using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Testnt.Common.Mappings;
using Testnt.Main.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestScenarios.Item.Query
{
    public class GetTestScenarioItemQuery : IRequest<GetTestScenarioItemDto>
    {
        public Guid Id { get; set; }
    }

    public class GetTestScenarioItemDto : IMapFrom<TestScenario>
    {
        public GetTestScenarioItemDto()
        {
            //TestStep = new List<GetTestCaseItemDto>();
        }
        public string Name { get; set; }
        public Guid Id { get; set; }
        //public List<GetTestCaseItemDto> TestStep { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TestScenario, GetTestScenarioItemDto>();
        }
    }

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


