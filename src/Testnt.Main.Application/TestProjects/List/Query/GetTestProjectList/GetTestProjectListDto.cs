using AutoMapper;
using Testnt.Common.Mappings;
using Testnt.Main.Domain.Entity;
using System;


namespace Testnt.Main.Application.TestProjects.List.Query.GetTestProjectList
{
    public class GetTestProjectListDto : IMapFrom<TestProject>
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TestProject, GetTestProjectListDto>();
        }
    }
}