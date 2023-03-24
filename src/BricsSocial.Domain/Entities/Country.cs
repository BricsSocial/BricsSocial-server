using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Domain.Entities
{
    public sealed class Country : EntityBase
    {
        public string Name { get; set; }

        public List<Specialist> Specialists { get; set; } = new List<Specialist>();
        public List<Company> Companies { get; set; } = new List<Company>();

        public static class Invariants
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 200;
        }
    }
}
