using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Domain.Entities
{
    public sealed class SkillTag
    {
        public static class Invariants
        {
            public const int SkillTagMinLength = 2;
            public const int SkillTagMaxLength = 40;
            public const int SkillTagsMinLength = 0;
            public const int SkillTagsMaxLength = 409;
            public const int SkillTagsMaxCount = 10;
        }
    }
}
