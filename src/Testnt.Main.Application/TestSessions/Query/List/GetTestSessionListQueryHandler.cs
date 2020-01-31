﻿using AutoMapper;
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

namespace Testnt.Main.Application.TestSessions.Query.List
{
    public class GetTestSessionListQueryHandler : IRequestHandler<GetTestSessionListQuery, GetObjectListVm<GetTestSessionListDto>>
    {
        private readonly TestntDbContext context;
        private readonly IMapper mapper;

        public GetTestSessionListQueryHandler(TestntDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GetObjectListVm<GetTestSessionListDto>> Handle(GetTestSessionListQuery request, CancellationToken cancellationToken)
        {
            var sessions = await context.Sessions
                .Where(t => t.ProjectId == request.ProjectId)
                .ProjectTo<GetTestSessionListDto>(mapper.ConfigurationProvider)
                //.OrderBy(t => t.)
                .ToListAsync(cancellationToken);

            var vm = new GetObjectListVm<GetTestSessionListDto>
            {
                Data = sessions,
                Count = sessions.Count
            };

            return vm;
        }
    }
}
