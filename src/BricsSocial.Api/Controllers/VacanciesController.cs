using BricsSocial.Api.Swagger;
using BricsSocial.Application.Common.Models;
using BricsSocial.Application.Replies.Common;
using BricsSocial.Application.Replies.CreateVacancyReply;
using BricsSocial.Application.Replies.GetReplies;
using BricsSocial.Application.Replies.GetSpecialistReplies;
using BricsSocial.Application.Replies.UpdateVacancyReply;
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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginatedList<VacancyDto>>> Get([FromQuery] GetVacanciesQuery request, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VacancyDto>> GetById(int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetVacancyQuery { Id = id }, cancellationToken));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VacancyDto>> Create(CreateVacancyCommand request, CancellationToken cancellationToken)
        {
            return StatusCode(StatusCodes.Status201Created, await Mediator.Send(request, cancellationToken));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VacancyDto>> Update(int id, UpdateVacancyCommand request, CancellationToken cancellationToken)
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
        [RequestType(typeof(DeleteVacancyCommand))]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteVacancyCommand { Id = id }, cancellationToken);

            return NoContent();
        }

        [HttpPost("replies")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RequestType(typeof(CreateVacancyReplyCommand))]
        public async Task<ActionResult<ReplyDto>> CreateReply(CreateVacancyReplyCommand request, CancellationToken cancellationToken)
        {
            return StatusCode(StatusCodes.Status201Created, await Mediator.Send(request, cancellationToken));
        }

        [HttpPut("replies/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReplyDto>> UpdateReply(int id, UpdateVacancyReplyCommand request, CancellationToken cancellationToken)
        {
            if (request.Id != id)
                return BadRequest();

            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpGet("replies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginatedList<ReplyDto>>> GetReplies([FromQuery] GetVacancyRepliesQuery request, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }
    }
}