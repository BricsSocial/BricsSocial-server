using BricsSocial.Domain.Entities;
using BricsSocial.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Vacancies.Common
{
    public sealed class VacancyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Requirements { get; set; }
        public string Offerings { get; set; }
        public VacancyStatus Status { get; set; }

        public int CompanyId { get; set; }
    }
}
