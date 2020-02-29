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
using Testnt.Common.Mappings;
using Testnt.Main.Application.Common;
using Testnt.Main.Application.Components.ProjectComponents.Common;
using Testnt.Main.Domain.Entity;
using Testnt.Main.Domain.Entity.Projects;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.Components.ProjectComponents.Features.Query.List
{
    public class GetProjectFeatureListQuery : ProjectComponentRequest, IRequest<GetObjectListVm<GetProjectFeatureListDto>>
    {
    }

    public class GetProjectFeatureListQueryHandler : IRequestHandler<GetProjectFeatureListQuery, GetObjectListVm<GetProjectFeatureListDto>>
    {
        private readonly TestntDbContext context;
        private readonly IMapper mapper;

        public GetProjectFeatureListQueryHandler(TestntDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GetObjectListVm<GetProjectFeatureListDto>> Handle(GetProjectFeatureListQuery request, CancellationToken cancellationToken)
        {
            var testFeaturesFromDb = await context.Features
                .Where(t => t.Project.Id.Equals(request.ProjectId))
                .ProjectTo<GetProjectFeatureListDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new GetObjectListVm<GetProjectFeatureListDto>
            {
                Data = testFeaturesFromDb,
                Count = testFeaturesFromDb.Count
            };

            return vm;
        }
    }

    public class GetProjectFeatureListDto : IMapFrom<Feature>
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        //public List<GetTestCaseItemDto> TestStep { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Scenario, GetProjectFeatureListDto>();
        }
    }
}
