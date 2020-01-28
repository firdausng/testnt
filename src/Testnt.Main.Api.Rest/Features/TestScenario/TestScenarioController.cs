using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testnt.Main.Api.Rest.Extensions;
using Testnt.Main.Application.Common;
using Testnt.Main.Application.TestScenarios.Item.Command;
using Testnt.Main.Application.TestScenarios.Item.Query;
using Testnt.Main.Application.TestScenarios.List.Query;

namespace Testnt.Main.Api.Rest.Features.TestScenario
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TestScenarioController : ControllerBase
    {
        private readonly IMediator mediator;

        public TestScenarioController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(Name = "GetTestScenarios")]
        public async Task<ActionResult<GetObjectListVm<GetTestScenarioListDto>>> GetTestScenarios()
        {
            var vm = await mediator.Send(new GetTestScenarioListQuery(HttpContext.GetTenantId()));
            return Ok(vm);
        }

        [HttpGet("{testscenarioId}", Name = "GetTestScenario")]
        public async Task<ActionResult<GetTestScenarioListDto>> GetTestScenario(Guid testscenarioId)
        {
            var vm = await mediator.Send(new GetTestScenarioItemQuery(HttpContext.GetTenantId()) { Id = testscenarioId });

            return Ok(vm);
        }

        [HttpPost(Name = "NewTestScenario")]
        public async Task<ActionResult<Guid>> NewTestCase(CreateTestScenarioItemCommand createTestScenarioItemCommand)
        {
            var vm = await mediator.Send(createTestScenarioItemCommand);
            if (vm.Id != null)
            {
                var link = Url.Link("GetTestScenario", new { testscenarioId = vm.Id });
                return Created(link, vm);
            }
            else
            {
                return BadRequest(vm);
            }

        }
    }
}
