using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Domain.Entities
{
    public sealed class Specialist : UserBase
    {
        public string Bio { get; set; }
        public string About { get; set; }
        public string SkillTags { get; set; }

        public string? Photo { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }
        public List<Reply> Replies { get; set; } = new List<Reply>();

        public static class Invariants
        {
            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 100;
            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 100;

            public const int BioMinLength = 0;
            public const int BioMaxLength = 70;
            public const int AboutMinLength = 0;
            public const int AboutMaxLength = 10000;

            public const int PhotoMinLength = 1;
            public const int PhotoMaxLength = 2 << 20;
        }
    }
}
