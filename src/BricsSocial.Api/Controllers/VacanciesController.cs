using BricsSocial.Api.Swagger;
using BricsSocial.Application.Common.Models;
using BricsSocial.Application.Vacancies.Common;
using BricsSocial.Application.Vacancies.CreateVacancy;
using BricsSocial.Application.Vacancies.DeleteVacancy;
using BricsSocial.Application.Vacancies.GetVacancies;
using BricsSocial.Application.Vacancies.GetVacancy;
using BricsSocial.Application.Vacancies.UpdateVacancy;
using Microsoft.AspNetCore.Mvc;

namespace BricsSocial.Api.Controllers
{
    public class VacanciesController : ApiControllerBase
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VacancyDto>> GetVacancy(int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetVacancyQuery { Id = id }, cancellationToken));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginatedList<VacancyDto>>> GetVacancies([FromQuery] GetVacanciesQuery request, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VacancyDto>> Create(CreateVacancyCommand command, CancellationToken cancellationToken)
        {
            return StatusCode(StatusCodes.Status201Created, await Mediator.Send(command, cancellationToken));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VacancyDto>> UpdateVacancy(int id, UpdateVacancyCommand request, CancellationToken cancellationToken)
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
        [RequestType(typeof(DeleteVacancyCommand))]
        public async Task<IActionResult> DeleteVacancy(int id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteVacancyCommand { Id = id }, cancellationToken);

            return NoContent();
        }
    }
}