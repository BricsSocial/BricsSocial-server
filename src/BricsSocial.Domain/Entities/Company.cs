using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Domain.Entities
{
    public sealed class Company : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Logo { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public List<Agent> Agents { get; set; }
        public List<Vacancy> Vacancies { get; set; }
    }
}
