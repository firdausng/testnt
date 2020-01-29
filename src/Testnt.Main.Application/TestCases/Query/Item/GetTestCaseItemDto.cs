using AutoMapper;
using Testnt.Main.Domain.Entity;
using System;
using System.Collections.Generic;
using Testnt.Common.Mappings;

namespace Testnt.Main.Application.TestCases.Query.Item
{
    public class GetTestCaseItemDto : IMapFrom<TestCase>
    {
        public GetTestCaseItemDto()
        {
            TestStep = new List<GetTestCaseStepItemDto>();
        }
        public string Name { get; set; }
        public Guid Id { get; set; }
        public Guid TestScenarioId { get; set; }
        public List<GetTestCaseStepItemDto> TestStep { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TestCase, GetTestCaseItemDto>();
        }
    }

    public class GetTestCaseStepItemDto : IMapFrom<TestStep>
    {
        public string Description { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TestStep, GetTestCaseStepItemDto>();
        }
    }
}
