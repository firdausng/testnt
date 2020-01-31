using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Testnt.Common.Mappings;
using Testnt.Main.Domain.Entity.TestSessionEntity;

namespace Testnt.Main.Application.TestSessions.Query.List
{
    public class GetTestSessionListDto : IMapFrom<Session>
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public Guid TestProjectId { get; set; }
        public DateTimeOffset Started { get; set; }
        public DateTimeOffset Finished { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Session, GetTestSessionListDto>();
        }
    }
}
