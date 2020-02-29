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
using Testnt.Main.Application.Components.ProjectComponents.Common;
using Testnt.Main.Domain.Entity;
using Testnt.Main.Domain.Entity.Projects;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.Components.ProjectComponents.Features.Query.Item
{
    public class GetProjectFeatureItemQuery : ProjectComponentRequest, IRequest<GetProjectFeatureItemDto>
    {
        public Guid Id { get; set; }
    }

    public class GetProjectFeatureItemQueryHandler : IRequestHandler<GetProjectFeatureItemQuery, GetProjectFeatureItemDto>
    {
        private readonly TestntDbContext context;
        private readonly IMapper mapper;

        public GetProjectFeatureItemQueryHandler(TestntDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GetProjectFeatureItemDto> Handle(GetProjectFeatureItemQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Features
                .Where(t => t.Project.Id == request.ProjectId)
                .Where(t => t.Id == request.Id)
                .Include(t => t.Scenarios)
                .ProjectTo<GetProjectFeatureItemDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new EntityNotFoundException(nameof(Feature), request.Id);
            }

            return entity;
        }
    }

    public class GetProjectFeatureItemDto
    {
    }
}
