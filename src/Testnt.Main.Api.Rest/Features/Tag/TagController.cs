using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Testnt.Main.Application.Common;
using Testnt.Main.Application.Components.ProjectComponents.Tags.Command.Create;
using Testnt.Main.Application.Components.ProjectComponents.Tags.Query.Item;
using Testnt.Main.Application.Components.ProjectComponents.Tags.Query.List;

namespace Testnt.Main.Api.Rest.Features.Tag
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult<GetObjectListVm<GetTestTagListDto>>> GetTags()
        {
            var vm = await mediator.Send(new GetProjectTagListQuery());
            return Ok(vm);
        }

        [HttpGet("{tagId}", Name = "GetTag")]
        public async Task<ActionResult<GetTestTagItemDto>> GetTag(Guid tagId)
        {
            var vm = await mediator.Send(new GetProjectTagItemQuery() { Id = tagId });

            return Ok(vm);
        }


        [HttpPost(Name = "NewTag")]
        public async Task<ActionResult<Guid>> NewProject(CreateTagItemCommand createTestTagItemCommand)
        {
            var vm = await mediator.Send(createTestTagItemCommand);
            if (vm.Id != null)
            {
                var link = Url.Link("GetTag", new { tagId = vm.Id });
                return Created(link, vm);
            }
            else
            {
                return BadRequest(vm);
            }

        }
    }
}