﻿using AutoMapper;
using Testnt.Common.Mappings;
using Testnt.Main.Domain.Entity;
using System;
using Testnt.Main.Domain.Entity.Projects;

namespace Testnt.Main.Application.Components.ProjectComponents.Scenarios.Query.List
{
    public class GetProjectScenarioListDto : IMapFrom<Scenario>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Scenario, GetProjectScenarioListDto>();
        }
    }
}
