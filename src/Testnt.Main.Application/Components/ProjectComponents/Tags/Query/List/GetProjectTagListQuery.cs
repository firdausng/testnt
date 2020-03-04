using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Common.Mappings;
using Testnt.Common.Models;
using Testnt.Main.Application.Components.ProjectComponents.Common;
using Testnt.Main.Domain.Entity;
using Testnt.Main.Domain.Entity.Projects;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.Components.ProjectComponents.Tags.Query.List
{
    public class GetProjectTagListQuery : ProjectComponentRequest, IRequest<GetObjectListVm<GetTestTagListDto>>
    {
    }

    public class GetTestTagListQueryHandler : IRequestHandler<GetProjectTagListQuery, GetObjectListVm<GetTestTagListDto>>
    {
        private readonly TestntDbContext context;
        private readonly IMapper mapper;

        public GetTestTagListQueryHandler(TestntDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GetObjectListVm<GetTestTagListDto>> Handle(GetProjectTagListQuery request, CancellationToken cancellationToken)
        {
            var testTagsFromDb = await context.Tags
                .Where(t => t.ProjectId.Equals(request.ProjectId))
                .ProjectTo<GetTestTagListDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new GetObjectListVm<GetTestTagListDto>
            {
                Data = testTagsFromDb,
                Count = testTagsFromDb.Count
            };

            return vm;
        }
    }

    public class GetTestTagListDto : IMapFrom<Tag>
    {
        public string Name { get; set; }
        public Guid ProjectId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Tag, GetTestTagListDto>();
        }
    }
}
