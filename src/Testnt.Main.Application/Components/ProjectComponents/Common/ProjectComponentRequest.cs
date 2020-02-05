using System;
using Testnt.Main.Domain.Entity;

namespace Testnt.Main.Application.Components.ProjectComponents.Common
{
    public abstract class ProjectComponentRequest
    {
        public Guid ProjectId { get; set; }

        public TEntity AddProjectId<TEntity>(TEntity entity, Guid projectId) where TEntity: ProjectComponentEntity
        {
            entity.ProjectId = projectId;
            return entity;
        }
    }
}
