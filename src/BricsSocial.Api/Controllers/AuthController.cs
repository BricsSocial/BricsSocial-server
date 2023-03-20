using BricsSocial.Application.Auth.Login;
using Microsoft.AspNetCore.Mvc;

namespace BricsSocial.Api.Controllers
{
    public class AuthController : ApiControllerBase
    {
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TokenResponse>> Login(LoginCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }
    }
}
