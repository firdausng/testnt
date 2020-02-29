using AutoMapper;
using Testnt.Common.Mappings;
using Testnt.Main.Domain.Entity;
using System;
using Testnt.Main.Domain.Entity.Projects;

namespace Testnt.Main.Application.Components.ProjectComponents.Scenarios.Query.Item
{
    public class GetProjectScenarioItemDto : IMapFrom<Scenario>
    {
        public GetProjectScenarioItemDto()
        {
            //TestStep = new List<GetTestCaseItemDto>();
        }
        public string Name { get; set; }
        public Guid Id { get; set; }
        //public List<GetTestCaseItemDto> TestStep { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Scenario, GetProjectScenarioItemDto>();
        }
    }
}


