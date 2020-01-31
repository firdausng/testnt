using AutoMapper;
using Testnt.Main.Domain.Entity;
using System;
using Testnt.Common.Mappings;

namespace Testnt.Main.Application.TestProjects.Query.Item
{
    public class GetTestProjectItemDto : IMapFrom<Project>
    {
        public string Name { get; set; }
        public Guid Id { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Project, GetTestProjectItemDto>();
        }
    }

    
}

