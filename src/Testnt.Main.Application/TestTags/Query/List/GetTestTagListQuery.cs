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
using Testnt.Main.Domain.Entity;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestTags.Query.List
{
    public class GetTestTagListQuery : BaseRequest, IRequest<GetObjectListVm<GetTestTagListDto>>
    {
        public Guid ProjectId { get; set; }
    }

    public class GetTestTagListQueryHandler : IRequestHandler<GetTestTagListQuery, GetObjectListVm<GetTestTagListDto>>
    {
        private readonly TestntDbContext context;
        private readonly IMapper mapper;

        public GetTestTagListQueryHandler(TestntDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GetObjectListVm<GetTestTagListDto>> Handle(GetTestTagListQuery request, CancellationToken cancellationToken)
        {
            var testTagsFromDb = await context.TestTags
                //.Where(t => t.TenantId.Equals(request.TenantId))
                .Where(t => t.ProjectId.Equals(request.ProjectId))
                .ProjectTo<GetTestTagListDto>(mapper.ConfigurationProvider)
                //.OrderBy(t => t.)
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
