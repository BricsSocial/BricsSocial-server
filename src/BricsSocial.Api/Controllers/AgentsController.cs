using BricsSocial.Application.Agents.Common;
using BricsSocial.Application.Agents.GetAgent;
using BricsSocial.Application.Agents.GetAgents;
using BricsSocial.Application.Agents.RegisterAgent;
using BricsSocial.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace BricsSocial.Api.Controllers
{
    public sealed class AgentsController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<AgentDto>> Create(CreateAgentCommand request, CancellationToken cancellationToken)
        {
            var agent = await Mediator.Send(request, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, agent);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<AgentDto>>> Get([FromQuery] GetAgentsQuery request, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AgentDto>> GetById(int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAgentQuery { Id = id }, cancellationToken));
        }
    }
}
