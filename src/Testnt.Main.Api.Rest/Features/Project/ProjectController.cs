using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Testnt.Common.Models;
using Testnt.Main.Application.Components.ProjectComponents.Projects.Command.Create;
using Testnt.Main.Application.Components.ProjectComponents.Projects.Command.Delete;
using Testnt.Main.Application.Components.ProjectComponents.Projects.Query.Item;
using Testnt.Main.Application.Components.ProjectComponents.Projects.Query.List;

namespace Testnt.Main.Api.Rest.Features.Project
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProjectController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(Name = "GetProjects")]
        public async Task<ActionResult<GetObjectListVm<GetProjectListDto>>> GetProjects()
        {
            var vm = await mediator.Send(new GetProjectListQuery());
            return Ok(vm);
        }

        [HttpGet("{projectId}", Name = "GetProject")]
        public async Task<ActionResult<GetProjectItemDto>> GetProject(Guid projectId)
        {
            var vm = await mediator.Send(new GetProjectItemQuery() { Id = projectId });

            return Ok(vm);
        }

        [HttpPost(Name = "NewProject")]
        public async Task<ActionResult<Guid>> NewProject(CreateProjectItemCommand createProjectItemCommand)
        {
            var vm = await mediator.Send(createProjectItemCommand);
            if (vm.Id != null)
            {
                var link = Url.Link("GetProject", new { projectId = vm.Id });
                return Created(link, vm);
            }
            else
            {
                return BadRequest(vm);
            }

        }

        [HttpDelete("{id}", Name = "DeleteProject")]
        public async Task<ActionResult> DeleteProject(Guid id)
        {
            await mediator.Send(new DeleteProjectItemCommand() { Id = id });
            return NoContent();

        }
    }
}