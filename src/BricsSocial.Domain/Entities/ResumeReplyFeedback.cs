
using BricsSocial.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Domain.Entities
{
    public sealed class ResumeReplyFeedback : EntityBase
    {
        public string Message { get; set; }
        public FeedbackStatus Status { get; set; }

        public int ResumeReplyId { get; set; }
        public ResumeReply ResumeReply { get; set; }
    }
}
