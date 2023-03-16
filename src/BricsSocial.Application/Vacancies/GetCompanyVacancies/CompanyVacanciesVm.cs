using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BricsSocial.Application.Vacancies.Dtos;

namespace BricsSocial.Application.Vacancies.GetCompanyVacancies
{
    public sealed class CompanyVacanciesVm
    {
        public CompanyDto Company { get; init; }

        public IReadOnlyCollection<VacancyDto> Vacancies { get; init; } = Array.Empty<VacancyDto>();
    }
}
