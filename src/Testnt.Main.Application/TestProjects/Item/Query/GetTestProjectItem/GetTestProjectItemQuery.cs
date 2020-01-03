using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Testnt.Common.Exceptions;
using Testnt.Main.Domain.Entity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestProjects.Item.Query.GetTestProjectItem
{
    public class GetTestProjectItemQuery : IRequest<GetTestProjectItemDto>
    {
        public Guid Id { get; set; }

        public class GetProjectItemQueryHandler : IRequestHandler<GetTestProjectItemQuery, GetTestProjectItemDto>
        {
            private readonly TestntDbContext context;
            private readonly IMapper mapper;

            public GetProjectItemQueryHandler(TestntDbContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetTestProjectItemDto> Handle(GetTestProjectItemQuery request, CancellationToken cancellationToken)
            {
                var project = await context.Projects
                    .Where(t => t.Id == request.Id)
                    //.SingleAsync()
                    .ProjectTo<GetTestProjectItemDto>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
                    ;

                if (project.Count == 0)
                {
                    throw new EntityNotFoundException(nameof(TestProject), request.Id);
                }

                return project.Single();
            }
        }
    }

    
}

