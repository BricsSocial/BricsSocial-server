using BricsSocial.Application.Auth.Login;
using Microsoft.AspNetCore.Mvc;

namespace BricsSocial.Api.Controllers
{
    public class AuthController : ApiControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<TokenResponse>> Login(LoginCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }
    }
}
