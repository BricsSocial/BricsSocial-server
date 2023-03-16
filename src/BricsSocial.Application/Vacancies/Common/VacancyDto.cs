using BricsSocial.Domain.Entities;
using BricsSocial.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Vacancies.Common
{
    internal class VacancyDto
    {
        public string Name { get; set; }
        public string Requirements { get; set; }
        public string Offerings { get; set; }
        public int Status { get; set; }

        public int CompanyId { get; set; }
    }
}
