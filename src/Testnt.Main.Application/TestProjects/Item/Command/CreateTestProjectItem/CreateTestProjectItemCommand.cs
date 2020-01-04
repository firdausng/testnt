﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Testnt.Main.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Application.TestProjects.Item.Command.CreateTestProjectItem
{
    public class CreateTestProjectItemCommand : IRequest<CreateTestProjectItemDto>
    {
        public string Name { get; set; }
        public class CreateProjectItemCommandHandler : IRequestHandler<CreateTestProjectItemCommand, CreateTestProjectItemDto>
        {
            private readonly TestntDbContext context;
            public CreateProjectItemCommandHandler(TestntDbContext context)
            {
                this.context = context;
            }

            public async Task<CreateTestProjectItemDto> Handle(CreateTestProjectItemCommand request, CancellationToken cancellationToken)
            {
                var entity = new TestProject { Name = request.Name };
                context.Projects.Add(entity);
                await context.SaveChangesAsync(cancellationToken);

                return new CreateTestProjectItemDto
                {
                    Id = entity.Id
                };
            }
        }
    }
}