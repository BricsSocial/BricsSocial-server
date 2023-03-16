using BricsSocial.Api.Swagger;
using BricsSocial.Application.Vacancies.Commands.CreateVacancy;
using BricsSocial.Application.Vacancies.Commands.DeleteVacancy;
using BricsSocial.Application.Vacancies.Queries.Dtos;
using BricsSocial.Application.Vacancies.Queries.GetCompanyVacancies;
using BricsSocial.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BricsSocial.Api.Controllers
{
    public class VacanciesController : ApiControllerBase
    {
        [HttpPost]
        public async Task<int> CreateVacancy(CreateVacancyCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<int> DeleteVacancy(int id)
        {
            return await Mediator.Send(new DeleteVacancyCommand
            {
                VacancyId = id
            });
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