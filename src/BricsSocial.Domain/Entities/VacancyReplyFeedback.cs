using BricsSocial.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Domain.Entities
{
    public sealed class VacancyReplyFeedback : EntityBase
    {
        public string Message { get; set; }
        public FeedbackStatus Status { get; set; }

        public int AgentId { get; set; }
        public Agent Agent { get; set; }
        public int VacancyReplyId { get; set; }
        public VacancyReply VacancyReply { get; set; }

        public static class Invariants
        {
            public const int MessageMinLength = 2;
            public const int MessageMaxLength = 1500;
        }
    }
}
