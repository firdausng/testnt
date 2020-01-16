using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Testnt.Main.Application.TestProjects.Item.Command.CreateTestProjectItem;
using Testnt.Main.Application.TestProjects.Item.Query.GetTestProjectItem;
using Testnt.Main.Application.TestProjects.List.Query.GetTestProjectList;

namespace Testnt.Main.Api.Rest.Controllers.Project
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<ActionResult<GetTestProjectListVm>> GetUsers()
        {
            var vm = await mediator.Send(new GetTestProjectListQuery());
            return Ok(vm);
        }

        [HttpGet("{projectId}", Name = "GetUser")]
        public async Task<ActionResult<GetTestProjectItemDto>> GetUser(Guid projectId)
        {
            var vm = await mediator.Send(new GetTestProjectItemQuery { Id = projectId });

            return Ok(vm);
        }

        [HttpPost(Name = "NewUser")]
        public async Task<ActionResult<Guid>> NewUser(CreateTestProjectItemCommand createProjectItemCommand)
        {
            var vm = await mediator.Send(createProjectItemCommand);
            if (vm.Id != null)
            {
                var link = Url.Link("GetUser", new { projectId = vm.Id });
                return Created(link, vm);
            }
            else
            {
                return BadRequest(vm);
            }

        }
    }
}