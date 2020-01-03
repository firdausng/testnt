using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testnt.Main.Application.TestCases.Item.Command.CreateTestCaseItem;
using Testnt.Main.Application.TestCases.Item.Query.GetTestCaseItem;
using Testnt.Main.Application.TestCases.List.Query.GetTestCaseList;

namespace Testnt.Main.Api.Rest.Features.TestCase
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TestCaseController : ControllerBase
    {
        private readonly IMediator mediator;

        public TestCaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(Name = "GetTestCases")]
        public async Task<ActionResult<GetTestCaseListVm>> GetTestCases()
        {
            var vm = await mediator.Send(new GetTestCaseListQuery());
            return Ok(vm);
        }

        [HttpGet("{testcaseId}", Name = "GetTestCase")]
        public async Task<ActionResult<GetTestCaseListDto>> GetTestCase(Guid testcaseId)
        {
            var vm = await mediator.Send(new GetTestCaseItemQuery { Id = testcaseId });

            return Ok(vm);
        }

        [HttpPost(Name = "NewTestCase")]
        public async Task<ActionResult<Guid>> NewTestCase(CreateTestCaseItemCommand createTestCaseItemCommand)
        {
            var vm = await mediator.Send(createTestCaseItemCommand);
            if (vm.Id != null)
            {
                var link = Url.Link("GetTestCase", new { testcaseId = vm.Id });
                return Created(link, vm);
            }
            else
            {
                return BadRequest(vm);
            }

        }
    }
}
