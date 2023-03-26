using BricsSocial.Api.Swagger;
using BricsSocial.Application.Auth.Current;
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

        [HttpGet("current")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RequestType(typeof(GetCurrentUserQuery))]
        public async Task<ActionResult<CurrentUserDto>> Current(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetCurrentUserQuery(), cancellationToken));
        }
    }
}
