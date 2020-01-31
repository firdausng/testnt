using AutoMapper;
using Testnt.Common.Mappings;
using Testnt.Main.Domain.Entity;
using System;


namespace Testnt.Main.Application.TestProjects.Query.List 
{ 
    public class GetTestProjectListDto : IMapFrom<Project>
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Project, GetTestProjectListDto>();
        }
    }
}