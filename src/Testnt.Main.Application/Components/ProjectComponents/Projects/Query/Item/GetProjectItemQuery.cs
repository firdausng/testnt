using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Testnt.Common.Exceptions;
using Testnt.Main.Domain.Entity;
using System;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Main.Infrastructure.Data;
using Testnt.Main.Domain.Entity.Projects;

namespace Testnt.Main.Application.Components.ProjectComponents.Projects.Query.Item
{
    public class GetProjectItemQuery :IRequest<GetProjectItemDto>
    {
        public Guid Id { get; set; }

        public class GetProjectItemQueryHandler : IRequestHandler<GetProjectItemQuery, GetProjectItemDto>
        {
            private readonly TestntDbContext context;
            private readonly IMapper mapper;

            public GetProjectItemQueryHandler(TestntDbContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<GetProjectItemDto> Handle(GetProjectItemQuery request, CancellationToken cancellationToken)
            {
                var project = await context.Projects
                    .ProjectTo<GetProjectItemDto>(mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken)
                    ;

                if (project == null)
                {
                    throw new EntityNotFoundException(nameof(Project), request.Id);
                }

                return project;
            }
        }
    }

    
}

