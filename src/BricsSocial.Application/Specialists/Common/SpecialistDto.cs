using BricsSocial.Application.Common.Mappings;
using BricsSocial.Application.SkillTags.Common;
using BricsSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Specialists.Common
{
    public sealed class SpecialistDto : IMapFrom<Specialist>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? Bio { get; set; }
        public string? Skills { get; set; }
        public string? Experience { get; set; }

        public string? Photo { get; set; }

        public int CountryId { get; set; }
        public List<SkillTagDto> SkillTags { get; set; } = new List<SkillTagDto>();
    }
}
