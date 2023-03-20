using BricsSocial.Application.Common.Mappings;
using BricsSocial.Application.SkillTags.Common;
using BricsSocial.Application.Specialists.Common;
using BricsSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Resumes.Common
{
    public sealed class ResumeDto : IMapFrom<Resume>
    {
        public int Id { get; set; }
        public string Skills { get; set; }
        public string Experience { get; set; }

        public int SpecialistId { get; set; }
        public SpecialistDto Specialist { get; set; }

        public List<SkillTagDto> SkillTags { get; set; } = new List<SkillTagDto>();
    }
}
