using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Application.TestSessions.Query.Item
{
    public class GetTestSessionItemQuery : IRequest<GetTestSessionItemDto>
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
    }
}
