using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Testnt.Main.Api.Rest.Extensions;
using Testnt.Main.Application.Common;
using Testnt.Main.Application.TestSessions.Item.Command.CreateTestSessionItem;
using Testnt.Main.Application.TestSessions.Item.Query;
using Testnt.Main.Application.TestSessions.List.Query.GetTestSessionList;

namespace Testnt.Main.Api.Rest.Features.TestSession
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TestSessionController : ControllerBase
    {
        private readonly IMediator mediator;

        public TestSessionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(Name = "GetTestSessions")]
        public async Task<ActionResult<GetObjectListVm<GetTestSessionListDto>>> GetSessions()
        {
            var vm = await mediator.Send(new GetTestSessionListQuery(HttpContext.GetTenantId()));
            return Ok(vm);
        }

        [HttpGet("{sessionId}", Name = "GetTestSession")]
        public async Task<ActionResult<GetTestSessionItemDto>> GetProject(Guid sessionId)
        {
            var vm = await mediator.Send(new GetTestSessionItemQuery(HttpContext.GetTenantId()) { Id = sessionId });

            return Ok(vm);
        }

        [HttpPost(Name = "NewTestSession")]
        public async Task<ActionResult<Guid>> NewProject(CreateTestSessionItemCommand createTestSessionItemCommand)
        {
            var vm = await mediator.Send(createTestSessionItemCommand);
            if (vm.Id != null)
            {
                var link = Url.Link("GetTestSession", new { sessionId = vm.Id });
                return Created(link, vm);
            }
            else
            {
                return BadRequest(vm);
            }

        }
    }
}