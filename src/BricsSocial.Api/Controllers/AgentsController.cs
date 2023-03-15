using BricsSocial.Application.Agents.Commands.Register;
using Microsoft.AspNetCore.Mvc;

namespace BricsSocial.Api.Controllers
{
    public sealed class AgentsController : ApiControllerBase
    {

        [HttpPost("register")]
        public async Task<int> Register(RegisterAgentCommand command, CancellationToken cancellationToken)
        {
            return await Mediator.Send(command, cancellationToken);
        }
    }
}
