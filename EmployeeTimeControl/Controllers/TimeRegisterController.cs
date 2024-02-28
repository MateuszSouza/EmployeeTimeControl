using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Core.Handlers.TimerRegister;

namespace EmployeeTimeControl.Controllers
{
    [ApiController]
    [Route("RegsterTimer")]
    public class TimeRegisterController : ControllerBase
    {
        [HttpPost("/TimerRegistar")]
        [Authorize]
        public async Task<ActionResult<dynamic>> TimerRegister(
            [FromServices] IMediator mediator,
            [FromBody] TimeRegisterCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
