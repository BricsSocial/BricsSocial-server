using BricsSocial.Domain.Entities;
using BricsSocial.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Vacancies.Queries.GetCompanyVacancies
{
    public sealed class VacancyDto
    {
        public string Id { get; set; }
        public string Name { get; init; }
        public string Requirements { get; init; }
        public string Offerings { get; init; }
        public VacancyStatusDto Status { get; set; }
    }
}
