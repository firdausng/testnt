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

namespace Testnt.Main.Application.TestProjects.Query.List
{
    public class GetTestProjectListQueryHandler : IRequestHandler<GetTestProjectListQuery, GetObjectListVm<GetTestProjectListDto>>
    {
        private readonly TestntDbContext context;
        private readonly IMapper mapper;

        public GetTestProjectListQueryHandler(TestntDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GetObjectListVm<GetTestProjectListDto>> Handle(GetTestProjectListQuery request, CancellationToken cancellationToken)
        {
            var projects = await context.Projects
                .ProjectTo<GetTestProjectListDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new GetObjectListVm<GetTestProjectListDto>
            {
                Data = projects,
                Count = projects.Count
            };
            return vm;
        }
    }
}
