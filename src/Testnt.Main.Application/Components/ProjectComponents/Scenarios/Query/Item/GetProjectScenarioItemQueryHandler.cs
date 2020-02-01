﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.Components.ProjectComponents.Scenarios.Query.Item
{
    public class GetProjectScenarioItemQueryHandler : IRequestHandler<GetProjectScenarioItemQuery, GetProjectScenarioItemDto>
    {
        private readonly TestntDbContext context;
        private readonly IMapper mapper;

        public GetProjectScenarioItemQueryHandler(TestntDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GetProjectScenarioItemDto> Handle(GetProjectScenarioItemQuery request, CancellationToken cancellationToken)
        {
            var testCase = await context.Scenarios
                .Where(t => t.Project.Id == request.ProjectId)
                .Where(t => t.Id == request.Id)
                .Include(t => t.Steps)
                .Include(t => t.Tags)
                //.SingleAsync()
                .ProjectTo<GetProjectScenarioItemDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
                ;

            return testCase.Single();
        }
    }
}

