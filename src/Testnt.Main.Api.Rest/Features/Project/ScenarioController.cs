using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testnt.Main.Application.Common;
using Testnt.Main.Application.TestScenarios.Command.Item;
using Testnt.Main.Application.TestScenarios.Query.Item;
using Testnt.Main.Application.TestScenarios.Query.List;

namespace Testnt.Main.Api.Rest.Features.Project.Scenario
{
    [Route("api/project/{projectId}/scenario")]
    [Authorize]
    [ApiController]
    public class ScenarioController : ControllerBase
    {
        private readonly IMediator mediator;

        public ScenarioController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(Name = "GetProjectScenarios")]
        public async Task<ActionResult<GetObjectListVm<GetTestScenarioListDto>>> GetProjectScenarios(Guid projectId)
        {
            var vm = await mediator.Send(new GetTestScenarioListQuery() { ProjectId= projectId });
            return Ok(vm);
        }

        [HttpGet("{projectScenarioId}", Name = "GetProjectScenario")]
        public async Task<ActionResult<GetTestScenarioListDto>> GetProjectScenario(Guid projectScenarioId, Guid projectId)
        {
            var vm = await mediator.Send(new GetTestScenarioItemQuery() { Id = projectScenarioId });

            return Ok(vm);
        }

        [HttpPost(Name = "NewProjectScenario")]
        public async Task<ActionResult<Guid>> NewProjectScenario(CreateTestScenarioItemCommand createTestScenarioItemCommand, Guid projectId)
        {
            createTestScenarioItemCommand.ProjectId = projectId;
            var vm = await mediator.Send(createTestScenarioItemCommand);
            if (vm.Id != null)
            {
                var link = Url.Link("GetProjectScenario", new { projectScenarioId = vm.Id, projectId= projectId });
                return Created(link, vm);
            }
            else
            {
                return BadRequest(vm);
            }

        }
    }
}
