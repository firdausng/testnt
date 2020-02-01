using AutoMapper;
using Testnt.Common.Mappings;
using Testnt.Main.Domain.Entity;
using System;


namespace Testnt.Main.Application.Components.ProjectComponents.Projects.Query.List 
{ 
    public class GetProjectListDto : IMapFrom<Project>
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Project, GetProjectListDto>();
        }
    }
}