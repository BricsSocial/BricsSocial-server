using BricsSocial.Api.Swagger;
using BricsSocial.Application.Vacancies.CreateVacancy;
using BricsSocial.Application.Vacancies.DeleteVacancy;
using BricsSocial.Application.Vacancies.Dtos;
using BricsSocial.Application.Vacancies.GetCompanyVacancies;
using BricsSocial.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BricsSocial.Api.Controllers
{
    public class VacanciesController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateVacancyCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<VacancyDto>> GetVacancy(int id)
        //{
        //    return Created(await Mediator.Send());
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacancy(int id)
        {
            await Mediator.Send(new DeleteVacancyCommand { Id = id });

            return NoContent();
        }

        [HttpGet("company/{companyId}")]
        [RequestType(typeof(GetCompanyVacanciesQuery))]
        public async Task<CompanyVacanciesVm> GetCompanyVacancies(int companyId)
        {
            return await Mediator.Send(new GetCompanyVacanciesQuery
            {
                CompanyId = companyId
            });
        }

        //[HttpGet]
        //public async Task<IEnumerable<VacancyDto>> Get()
        //{

        //}
    }
}