using BricsSocial.Application.Common.Mappings;
using BricsSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.SkillTags.Common
{
    public sealed class SkillTagDto : IMapFrom<SkillTag>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
