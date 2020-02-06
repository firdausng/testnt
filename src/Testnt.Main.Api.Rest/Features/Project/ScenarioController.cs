using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Testnt.Main.Application.Common;
using Testnt.Main.Application.Components.ProjectComponents.Scenarios.Command.Item;
using Testnt.Main.Application.Components.ProjectComponents.Scenarios.Query.Item;
using Testnt.Main.Application.Components.ProjectComponents.Scenarios.Query.List;

namespace Testnt.Main.Api.Rest.Features.Project
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
        public async Task<ActionResult<GetObjectListVm<GetProjectScenarioListDto>>> GetProjectScenarios(Guid projectId)
        {
            var vm = await mediator.Send(new GetProjectScenarioListQuery() { ProjectId= projectId });
            return Ok(vm);
        }

        [HttpGet("{projectScenarioId}", Name = "GetProjectScenario")]
        public async Task<ActionResult<GetProjectScenarioListDto>> GetProjectScenario(Guid projectScenarioId, Guid projectId)
        {
            var vm = await mediator.Send(new GetProjectScenarioItemQuery() { Id = projectScenarioId, ProjectId= projectId });

            return Ok(vm);
        }

        [HttpPost(Name = "NewProjectScenario")]
        public async Task<ActionResult<Guid>> NewProjectScenario(CreateScenarioItemCommand createTestScenarioItemCommand, Guid projectId)
        {
            createTestScenarioItemCommand.ProjectId = projectId;
            var vm = await mediator.Send(createTestScenarioItemCommand);
            if (vm.Id != null)
            {
                var link = Url.Link("GetProjectScenario", new { projectScenarioId = vm.Id, projectId });
                return Created(link, vm);
            }
            else
            {
                return BadRequest(vm);
            }

        }
    }
}
