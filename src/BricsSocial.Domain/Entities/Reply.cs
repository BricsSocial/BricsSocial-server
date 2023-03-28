using BricsSocial.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Domain.Entities
{
    public sealed class Reply : EntityBase
    {
        public int? AgentId { get; set; }
        public Agent Agent { get; set; }
        public int SpecialistId { get; set; }
        public Specialist Specialist { get; set; }
        
        public int VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }

        public ReplyStatus Status { get; set; }
        public ReplyType Type { get; set; }
    }
}
