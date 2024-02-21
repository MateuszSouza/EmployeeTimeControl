using Core.Handlers.IncludeEmployee;
using Core.Handlers.Login;
using Domain.Model;
using Domain.Token;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTimeControl.Controllers
{
    [ApiController]
    [Route("EmployeeController")]
    public class EmployeeController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public EmployeeController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        //[HttpGet("/GetToken")]
        //public string GetToken()
        //{

        //    var usuario = new Employee();
        //    usuario.EmployeeName = "batima";
        //    usuario.Email = "Batima@email";
        //    usuario.Roles = "fodao";
        //    string retorno = _tokenService.GeradorDeToken(usuario);

        //    return retorno;
        //}

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
        //[AllowAnonymous(true)]
        public async Task<ActionResult<dynamic>> Include(
            [FromServices] IMediator mediator,
            [FromBody] IncludeEmployeeCommandRequest includeEmployeeCommandRequest)
        {
            var response = await mediator.Send(includeEmployeeCommandRequest);
            return Ok(response);
        }
    }

}
