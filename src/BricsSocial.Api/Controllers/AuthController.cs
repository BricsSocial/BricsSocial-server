using BricsSocial.Application.Auth.Commands.Login;
using Microsoft.AspNetCore.Mvc;

namespace BricsSocial.Api.Controllers
{
    public class AuthController : ApiControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [HttpPost("login")]
        [ProducesDefaultResponseType]
        public async Task<TokenResponse> Login(LoginCommand loginCommand, CancellationToken cancellationToken)
        {
            return await Mediator.Send(loginCommand, cancellationToken);
        }

        

    }
}
