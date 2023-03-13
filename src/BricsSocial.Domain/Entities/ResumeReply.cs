using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Domain.Entities
{
    public sealed class ResumeReply : EntityBase
    {
        public string Message { get; set; }

        public int ResumeId { get; set; }
        public Resume Resume { get; set; }

        public int AgentId { get; set; }
        public Agent Agent { get; set; }
        public int? VacancyId { get; set; }
        public Vacancy? Vacancy { get; set; }

        public List<ResumeReplyFeedback> Feedbacks { get; set; }
    }
}
