using BricsSocial.Api.Swagger;
using BricsSocial.Application.Agents.Common;
using BricsSocial.Application.Agents.DeleteAgent;
using BricsSocial.Application.Agents.DeleteAgentPhotoCommand;
using BricsSocial.Application.Agents.GetAgent;
using BricsSocial.Application.Agents.GetAgents;
using BricsSocial.Application.Agents.GetCurrentAgent;
using BricsSocial.Application.Agents.RegisterAgent;
using BricsSocial.Application.Agents.UpdateAgent;
using BricsSocial.Application.Agents.UpdateAgentPhotoCommand;
using BricsSocial.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace BricsSocial.Api.Controllers
{
    public sealed class AgentsController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginatedList<AgentDto>>> Get([FromQuery] GetAgentsQuery request, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AgentDto>> GetById(int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAgentQuery { Id = id }, cancellationToken));
        }

        [HttpGet("current")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RequestType(typeof(GetCurrentAgentQuery))]
        public async Task<ActionResult<AgentDto>> GetCurrent(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetCurrentAgentQuery(), cancellationToken));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AgentDto>> Create(CreateAgentCommand request, CancellationToken cancellationToken)
        {
            var agent = await Mediator.Send(request, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, agent);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AgentDto>> Update(int id, UpdateAgentCommand request, CancellationToken cancellationToken)
        {
            if (id != request.Id)
                return BadRequest();

            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RequestType(typeof(DeleteAgentCommand))]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteAgentCommand { Id = id }, cancellationToken);

            return NoContent();
        }

        [HttpPut("{id}/photo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RequestType(typeof(UpdateAgentPhotoCommand))]
        public async Task<ActionResult<FileUploadResponse>> UpdatePhoto(int id, IFormFile file, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new UpdateAgentPhotoCommand { Id = id, File = file }, cancellationToken));
        }

        [HttpDelete("{id}/photo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RequestType(typeof(DeleteAgentPhotoCommand))]
        public async Task<ActionResult> DeletePhoto(int id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteAgentPhotoCommand { Id = id }, cancellationToken);

            return Ok();
        }
    }
}
