using AutoMapper;
using Testnt.Common.Mappings;
using Testnt.Main.Domain.Entity;
using System;

namespace Testnt.Main.Application.TestScenarios.Query.Item
{
    public class GetTestScenarioItemDto : IMapFrom<Scenario>
    {
        public GetTestScenarioItemDto()
        {
            //TestStep = new List<GetTestCaseItemDto>();
        }
        public string Name { get; set; }
        public Guid Id { get; set; }
        //public List<GetTestCaseItemDto> TestStep { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Scenario, GetTestScenarioItemDto>();
        }
    }
}


