using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Domain.Entities
{
    public sealed class SkillTag : EntityBase
    {
        public string Name { get; set; }

        public static class Invariants
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 40;
        }

        public List<Specialist> Specialists { get; set; } = new List<Specialist>();
        public List<Vacancy> Vacancies { get; set; } = new List<Vacancy>();
    }
}
