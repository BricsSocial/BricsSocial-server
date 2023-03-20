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

        public List<Agent> Agents { get; set; } = new List<Agent>();
        public List<Vacancy> Vacancies { get; set; } = new List<Vacancy>();


        public static class Invariants
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 200;
            public const int DescriptionMinLength = 1;
            public const int DescriptionMaxLength = 10000;
            public const int LogoMinLength = 1;
            public const int LogoMaxLength = 2 << 20;
        }
    }
}
