﻿using BricsSocial.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Domain.Entities
{
    public sealed class VacancyReplyFeedBack : EntityBase
    {
        public string Message { get; set; }
        public FeedbackStatus Status { get; set; }

        public string AgentId { get; set; }
        public Agent Agent { get; set; }
        public string VacancyReplyId { get; set; }
        public VacancyReply VacancyReply { get; set; }
    }
}
