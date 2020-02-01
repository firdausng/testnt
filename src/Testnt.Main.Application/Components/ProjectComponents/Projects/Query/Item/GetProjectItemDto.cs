using AutoMapper;
using Testnt.Main.Domain.Entity;
using System;
using Testnt.Common.Mappings;

namespace Testnt.Main.Application.Components.ProjectComponents.Projects.Query.Item
{
    public class GetProjectItemDto : IMapFrom<Project>
    {
        public string Name { get; set; }
        public Guid Id { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Project, GetProjectItemDto>();
        }
    }

    
}

