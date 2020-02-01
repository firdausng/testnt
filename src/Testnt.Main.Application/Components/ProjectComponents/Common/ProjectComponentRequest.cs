using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Application.Components.ProjectComponents.Common
{
    public abstract class ProjectComponentRequest
    {
        public Guid ProjectId { get; set; }
    }
}
