using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testnt.Common.Models;
using Testnt.Main.Application.Components.ProjectComponents.Features.Command.Create;
using Testnt.Main.Application.Components.ProjectComponents.Features.Query.Item;
using Testnt.Main.Application.Components.ProjectComponents.Features.Query.List;

namespace Testnt.Main.Api.Rest.Features.Project
{
    [Route("api/project/{projectId}/feature")]
    [Authorize]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly IMediator mediator;

        public FeatureController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(Name = "GetProjectFeatures")]
        public async Task<ActionResult<GetObjectListVm<GetProjectFeatureListDto>>> GetProjectFeatures(Guid projectId)
        {
            var vm = await mediator.Send(new GetProjectFeatureListQuery() { ProjectId = projectId });
            return Ok(vm);
        }

        [HttpGet("{featureId}", Name = "GetProjectFeature")]
        public async Task<ActionResult<GetProjectFeatureListDto>> GetProjectFeature(Guid featureId, Guid projectId)
        {
            var vm = await mediator.Send(new GetProjectFeatureItemQuery() { Id = featureId, ProjectId= projectId });

            return Ok(vm);
        }

        [HttpPost(Name = "NewProjectFeature")]
        public async Task<ActionResult<Guid>> NewProjectFeature(CreateFeatureItemCommand itemCommand, Guid projectId)
        {
            itemCommand.ProjectId = projectId;
            var vm = await mediator.Send(itemCommand);
            if (vm.Id != null)
            {
                var link = Url.Link("GetProjectFeature", new { featureId = vm.Id, projectId });
                return Created(link, vm);
            }
            else
            {
                return BadRequest(vm);
            }

        }
    }
}
