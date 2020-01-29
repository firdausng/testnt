using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestSessions.Query.Item
{
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
