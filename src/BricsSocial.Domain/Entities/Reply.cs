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
        public string SpecialistMessage { get; set; }
        public string AgentMessage { get; set; }

        public int? AgentId { get; set; }
        public Agent Agent { get; set; }
        public int? SpecialistId { get; set; }
        public Specialist Specialist { get; set; }
        
        public int? VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }

        public ReplyStatus ReplyStatus { get; set; }
        public ReplyType ReplyType { get; set; }

        public static class Invariants
        {
            public const int MessageMinLength = 2;
            public const int MessageMaxLength = 1500;
        }
    }
}
