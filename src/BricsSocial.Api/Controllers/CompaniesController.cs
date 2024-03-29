﻿using BricsSocial.Api.Swagger;
using BricsSocial.Application.Common.Models;
using BricsSocial.Application.Companies.Common;
using BricsSocial.Application.Companies.CreateCompany;
using BricsSocial.Application.Companies.DeleteCompany;
using BricsSocial.Application.Companies.GetCompanies;
using BricsSocial.Application.Companies.GetCompany;
using BricsSocial.Application.Companies.UpdateCompany;
using BricsSocial.Application.Companies.UpdateCompanyLogo;
using Microsoft.AspNetCore.Mvc;

namespace BricsSocial.Api.Controllers
{
    public sealed class CompaniesController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginatedList<CompanyDto>>> Get([FromQuery] GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CompanyDto>> GetById(int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetCompanyQuery { Id = id }, cancellationToken));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CompanyDto>> Create(CreateCompanyCommand command, CancellationToken cancellationToken)
        {
            return StatusCode(StatusCodes.Status201Created, await Mediator.Send(command, cancellationToken));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CompanyDto>> Update(int id, UpdateCompanyCommand request, CancellationToken cancellationToken)
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
        [RequestType(typeof(DeleteCompanyCommand))]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteCompanyCommand { Id = id }, cancellationToken);

            return NoContent();
        }

        [HttpPut("{id}/logo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RequestType(typeof(UpdateCompanyLogoCommand))]
        public async Task<ActionResult<FileUploadResponse>> UpdateLogo(int id, IFormFile file, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new UpdateCompanyLogoCommand { Id = id, File = file }, cancellationToken));
        }
    }
}
