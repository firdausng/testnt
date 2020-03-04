using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Testnt.Common.Models;
using Testnt.Main.Application.Components.ProjectComponents.Tags.Command.Create;
using Testnt.Main.Application.Components.ProjectComponents.Tags.Query.Item;
using Testnt.Main.Application.Components.ProjectComponents.Tags.Query.List;

namespace Testnt.Main.Api.Rest.Features.Project
{
    [Route("api/project/{projectId}/tag")]
    [Authorize]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly IMediator mediator;

        public TagController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(Name = "GetTags")]
        public async Task<ActionResult<GetObjectListVm<GetTestTagListDto>>> GetTags(Guid projectId)
        {
            var vm = await mediator.Send(new GetProjectTagListQuery() { ProjectId = projectId });
            return Ok(vm);
        }

        [HttpGet("{tagId}", Name = "GetTag")]
        public async Task<ActionResult<GetTestTagItemDto>> GetTag(Guid projectId, Guid tagId)
        {
            var vm = await mediator.Send(new GetProjectTagItemQuery() { ProjectId = projectId, Id = tagId });

            return Ok(vm);
        }


        [HttpPost(Name = "NewTag")]
        public async Task<ActionResult<Guid>> NewProject(Guid projectId, CreateTagItemCommand createTestTagItemCommand)
        {
            createTestTagItemCommand.ProjectId = projectId;
            var vm = await mediator.Send(createTestTagItemCommand);
            if (vm.Id != null)
            {
                var link = Url.Link("GetTag", new { tagId = vm.Id, projectId });
                return Created(link, vm);
            }
            else
            {
                return BadRequest(vm);
            }

        }
    }
}