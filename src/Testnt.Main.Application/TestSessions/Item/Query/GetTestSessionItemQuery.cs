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
using Testnt.Main.Domain.Entity.TestSessionEntity;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestSessions.Item.Query
{
    public class GetTestSessionItemQuery : IRequest<GetTestSessionItemDto>
    {
        public Guid Id { get; set; }
    }

    public class GetTestSessionItemDto : IMapFrom<TestSession>
    {
        public GetTestSessionItemDto()
        {
        }
        public string Name { get; set; }
        public Guid Id { get; set; }
        public Guid TestProjectId { get; set; }
        public DateTimeOffset Started { get; set; }
        public DateTimeOffset Finished { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TestSession, GetTestSessionItemDto>();
        }
    }

    public class GetTestSessionItemQueryHandler : IRequestHandler<GetTestSessionItemQuery, GetTestSessionItemDto>
    {
        private readonly TestntDbContext context;
        private readonly IMapper mapper;

        public GetTestSessionItemQueryHandler(TestntDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GetTestSessionItemDto> Handle(GetTestSessionItemQuery request, CancellationToken cancellationToken)
        {
            var testCase = await context.TestSessions
                .Where(t => t.Id == request.Id)
                //.Include(t => t.Project)
                .ProjectTo<GetTestSessionItemDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
                ;

            return testCase.Single();
        }
    }
}
