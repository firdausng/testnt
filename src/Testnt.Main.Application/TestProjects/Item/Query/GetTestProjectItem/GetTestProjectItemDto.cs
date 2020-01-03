using AutoMapper;
using Testnt.Main.Domain.Entity;
using System;
using Testnt.Common.Mappings;

namespace Testnt.Main.Application.TestProjects.Item.Query.GetTestProjectItem
{
    public class GetTestProjectItemDto : IMapFrom<TestProject>
    {
        public string Name { get; set; }
        public Guid Id { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TestProject, GetTestProjectItemDto>();
        }
    }

    
}

