using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Domain.Entities
{
    public sealed class Resume : EntityBase
    {
        public string Skills { get; set; }
        public string Experience { get; set; }

        public int SpecialistId { get; set; }
        public Specialist Specialist { get; set; }


        public List<SkillTag> SkillTags { get; set; } = new List<SkillTag>();
        public List<ResumeReply> ResumeReplies { get; set; } = new List<ResumeReply>();

        public static class Invariants
        {
            public const int SkillsMinLength = 1;
            public const int SkillsMaxLength = 10000;
            public const int ExperienceMinLength = 1;
            public const int ExperienceMaxLength = 10000;
        }
    }
}
