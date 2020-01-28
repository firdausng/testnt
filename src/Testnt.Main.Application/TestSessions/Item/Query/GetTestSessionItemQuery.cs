using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Application.TestSessions.Item.Query
{
    public class GetTestSessionItemQuery : BaseRequest, IRequest<GetTestSessionItemDto>
    {
        public Guid Id { get; set; }
    }
}
