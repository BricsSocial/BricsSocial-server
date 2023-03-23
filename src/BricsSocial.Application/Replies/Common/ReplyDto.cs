using BricsSocial.Application.Agents.Common;
using BricsSocial.Application.Common.Mappings;
using BricsSocial.Application.Specialists.Common;
using BricsSocial.Application.Vacancies.Common;
using BricsSocial.Domain.Entities;
using BricsSocial.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Replies.Common
{
    public sealed class ReplyDto : IMapFrom<Reply>
    {
        public int? AgentId { get; set; }
        public AgentDto Agent { get; set; }
        public int? SpecialistId { get; set; }
        public SpecialistDto Specialist { get; set; }

        public int? VacancyId { get; set; }
        public VacancyDto Vacancy { get; set; }

        public ReplyStatus Status { get; set; }
        public ReplyType Type { get; set; }
    }
}
