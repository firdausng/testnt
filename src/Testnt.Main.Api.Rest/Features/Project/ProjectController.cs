using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Testnt.Main.Application.Common;
using Testnt.Main.Application.TestProjects.Item.Command.CreateTestProjectItem;
using Testnt.Main.Application.TestProjects.Item.Command.DeleteTestProjectItem;
using Testnt.Main.Application.TestProjects.Item.Query.GetTestProjectItem;
using Testnt.Main.Application.TestProjects.List.Query.GetTestProjectList;

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
        public async Task<ActionResult<GetObjectListVm<GetTestProjectListDto>>> GetProjects()
        {
            var vm = await mediator.Send(new GetTestProjectListQuery());
            return Ok(vm);
        }

        [HttpGet("{projectId}", Name = "GetProject")]
        public async Task<ActionResult<GetTestProjectItemDto>> GetProject(Guid projectId)
        {
            var vm = await mediator.Send(new GetTestProjectItemQuery { Id = projectId });

            return Ok(vm);
        }

        [HttpPost(Name = "NewProject")]
        public async Task<ActionResult<Guid>> NewProject(CreateTestProjectItemCommand createProjectItemCommand)
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
            await mediator.Send(new DeleteTestProjectItemCommand { Id = id });
            return NoContent();

        }
    }
}