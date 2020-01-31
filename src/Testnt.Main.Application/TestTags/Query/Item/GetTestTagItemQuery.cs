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
using Testnt.Common.Exceptions;
using Testnt.Common.Mappings;
using Testnt.Main.Application.Common;
using Testnt.Main.Domain.Entity;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestTags.Query.Item
{
    public class GetTestTagItemQuery : IRequest<GetTestTagItemDto>
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }

        public class GetTestTagItemQueryHandler : IRequestHandler<GetTestTagItemQuery, GetTestTagItemDto>
        {
            private readonly TestntDbContext context;
            private readonly IMapper mapper;

            public GetTestTagItemQueryHandler(TestntDbContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetTestTagItemDto> Handle(GetTestTagItemQuery request, CancellationToken cancellationToken)
            {
                var tag = await context.Tags
                    .Where(t => t.ProjectId.Equals(request.ProjectId))
                    .ProjectTo<GetTestTagItemDto>(mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken)
                    ;

                if (tag == null)
                {
                    throw new EntityNotFoundException(nameof(Tag), request.Id);
                }

                return tag;
            }
        }
    }

    public class GetTestTagItemDto : IMapFrom<Tag>
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
    }
}
