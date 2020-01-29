using AutoMapper;
using Testnt.Main.Domain.Entity;
using System;
using Testnt.Common.Mappings;

namespace Testnt.Main.Application.TestCases.Query.List 
{
    public class GetTestCaseListDto : IMapFrom<TestCase>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid TestScenarioId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TestCase, GetTestCaseListDto>();
        }
    }
}
