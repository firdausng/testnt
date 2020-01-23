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

namespace Testnt.Main.Application.TestSessions.List.Query.GetTestSessionList
{
    public class GetTestSessionListQueryHandler : IRequestHandler<GetTestSessionListQuery, GetTestSessionListVm>
    {
        private readonly TestntDbContext context;
        private readonly IMapper mapper;

        public GetTestSessionListQueryHandler(TestntDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GetTestSessionListVm> Handle(GetTestSessionListQuery request, CancellationToken cancellationToken)
        {
            var sessions = await context.TestSessions
                .ProjectTo<GetTestSessionListDto>(mapper.ConfigurationProvider)
                //.OrderBy(t => t.)
                .ToListAsync(cancellationToken);

            var vm = new GetTestSessionListVm
            {
                TestSessions = sessions,
                Count = sessions.Count
            };

            return vm;
        }
    }
}
