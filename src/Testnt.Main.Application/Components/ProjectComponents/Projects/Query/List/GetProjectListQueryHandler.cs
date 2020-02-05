using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Main.Application.Common;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.Components.ProjectComponents.Projects.Query.List
{
    public class GetProjectListQueryHandler : IRequestHandler<GetProjectListQuery, GetObjectListVm<GetProjectListDto>>
    {
        private readonly TestntDbContext context;
        private readonly IMapper mapper;

        public GetProjectListQueryHandler(TestntDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GetObjectListVm<GetProjectListDto>> Handle(GetProjectListQuery request, CancellationToken cancellationToken)
        {
            var projects = await context.Projects
                .ProjectTo<GetProjectListDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new GetObjectListVm<GetProjectListDto>
            {
                Data = projects,
                Count = projects.Count
            };
            return vm;
        }
    }
}
