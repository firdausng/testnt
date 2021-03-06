﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Common.Exceptions;
using Testnt.Main.Domain.Entity;
using Testnt.Main.Domain.Entity.Projects;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.Components.ProjectComponents.Projects.Command.Update
{
    public class UpdateProjectItemCommand : IRequest
    {
        public Guid ProjectId { get; set; }
        public bool IsEnabled { get; set; }

        public class UpdateTestProjectItemCommandHandler : IRequestHandler<UpdateProjectItemCommand>
        {
            private readonly TestntDbContext context;
            public UpdateTestProjectItemCommandHandler(TestntDbContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(UpdateProjectItemCommand request, CancellationToken cancellationToken)
            {
                var project = await context.Projects
                    .Where(t => t.Id.Equals(request.ProjectId))
                    .SingleOrDefaultAsync(cancellationToken);

                if (project == null)
                {
                    throw new EntityNotFoundException(nameof(Project), request.ProjectId);
                }

                project.IsEnabled = request.IsEnabled;

                context.Projects.Update(project);
                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

    

    public class UpdateTestProjectItemDto
    {
    }
}
