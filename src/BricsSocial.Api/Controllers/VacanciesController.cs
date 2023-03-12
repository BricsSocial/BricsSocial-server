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

        [HttpGet]
        public IEnumerable<Vacancy> GetAll()
        {
            return new List<Vacancy>
            {
                new Vacancy
                {
                    Name = "Developer",
                }
            };
        }
    }
}