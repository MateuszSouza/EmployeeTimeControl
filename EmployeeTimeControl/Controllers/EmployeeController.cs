using Core.Handlers.IncludeEmployee;
using Core.Handlers.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTimeControl.Controllers
{
    [ApiController]
    [Route("EmployeeController")]
    public class EmployeeController : ControllerBase
    {
        
        [HttpPost("/Login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login(
            [FromServices] IMediator mediator,
            [FromBody] LoginCommandRequest loginRequest)
        {
            var response = await mediator.Send(loginRequest);
            return Ok(response);
        }

        [HttpPost("/IncludeEmployee")]
        //[Authorize(Roles = "mananger")]
        public async Task<ActionResult<dynamic>> Include(
            [FromServices] IMediator mediator,
            [FromBody] IncludeEmployeeCommandRequest includeEmployeeCommandRequest)
        {
            var response = await mediator.Send(includeEmployeeCommandRequest);
            return Ok(response);
        }
    }

}
