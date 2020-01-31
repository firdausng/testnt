using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Application.TestScenarios.Query.List
{
    public class GetTestScenarioListQuery : IRequest<GetObjectListVm<GetTestScenarioListDto>>
    {
        public Guid ProjectId { get; set; }
    }
}
