using AutoMapper;
using System;
using Testnt.Common.Mappings;
using Testnt.Main.Domain.Entity.TestSessionEntity;

namespace Testnt.Main.Application.TestSessions.Query.Item
{
    public class GetTestSessionItemDto : IMapFrom<TestSession>
    {
        public GetTestSessionItemDto()
        {
        }
        public string Name { get; set; }
        public Guid Id { get; set; }
        public Guid TestProjectId { get; set; }
        public DateTimeOffset Started { get; set; }
        public DateTimeOffset Finished { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TestSession, GetTestSessionItemDto>();
        }
    }
}
