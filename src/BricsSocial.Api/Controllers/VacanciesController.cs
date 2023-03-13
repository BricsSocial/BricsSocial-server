using BricsSocial.Application.Vacancies.Commands.CreateVacancy;
using BricsSocial.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BricsSocial.Api.Controllers
{
    public class VacanciesController : ApiControllerBase
    {

        private readonly ILogger<VacanciesController> _logger;

        public VacanciesController(ILogger<VacanciesController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<int> CreateVacancy(CreateVacancyCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}