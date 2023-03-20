using BricsSocial.Api.Swagger;
using BricsSocial.Application.Common.Models;
using BricsSocial.Application.Specialists.Common;
using BricsSocial.Application.Specialists.CreateSpecialist;
using BricsSocial.Application.Specialists.DeleteSpecialist;
using BricsSocial.Application.Specialists.GetSpecialist;
using BricsSocial.Application.Specialists.GetSpecialists;
using BricsSocial.Application.Specialists.UpdateSpecialist;
using Microsoft.AspNetCore.Mvc;

namespace BricsSocial.Api.Controllers
{
    public sealed class SpecialistsController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginatedList<SpecialistDto>>> Get([FromQuery] GetSpecialistsQuery request, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SpecialistDto>> Get(int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetSpecialistQuery { Id = id }, cancellationToken));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SpecialistDto>> Create(CreateSpecialistCommand request, CancellationToken cancellationToken)
        {
            var agent = await Mediator.Send(request, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, agent);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SpecialistDto>> Update(int id, UpdateSpecialistCommand request, CancellationToken cancellationToken)
        {
            if (id != request.Id)
                return BadRequest();

            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RequestType(typeof(DeleteSpecialistCommand))]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteSpecialistCommand { Id = id }, cancellationToken);

            return NoContent();
        }
    }
}
