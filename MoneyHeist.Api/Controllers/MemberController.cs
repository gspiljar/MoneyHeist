using MediatR;
using Microsoft.AspNetCore.Mvc;

using MoneyHeist.Api.ApiLogic.Members;
using MoneyHeist.Api.Infrastructure;

namespace MoneyHeist.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MemberController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddMember([FromBody] AddMemberHandler.Command command)
        {
            var result = await _mediator.Send(command);
            return result.ToApiResponse();
        }
    }
}
