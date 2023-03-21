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
        public string Skills { get; set; }
        public string Experience { get; set; }

        public string? Photo { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public List<SkillTag> SkillTags { get; set; } = new List<SkillTag>();
        public List<Reply> Replies { get; set; } = new List<Reply>();

        public static class Invariants
        {
            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 100;
            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 100;

            public const int BioMinLength = 0;
            public const int BioMaxLength = 70;
            public const int SkillsMinLength = 0;
            public const int SkillsMaxLength = 10000;
            public const int ExperienceMinLength = 0;
            public const int ExperienceMaxLength = 10000;

            public const int PhotoMinLength = 1;
            public const int PhotoMaxLength = 2 << 20;
        }
    }
}
