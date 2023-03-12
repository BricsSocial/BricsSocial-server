﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Vacancies.Queries.GetCompanyVacancies
{
    public sealed class CompanyVacanciesVm
    {
        public CompanyDto Company { get; init; }

        public IReadOnlyCollection<VacancyDto> Vacancies { get; init; } = Array.Empty<VacancyDto>();
    }
}