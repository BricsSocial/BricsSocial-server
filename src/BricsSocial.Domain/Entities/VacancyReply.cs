﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Domain.Entities
{
    public sealed class VacancyReply : EntityBase
    {
        public string Message { get; set; }

        public string SpecialistId { get; set; }
        public Specialist Specialist { get; set; }
        public string VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }

        public List<VacancyReplyFeedBack> FeedBacks { get; set; }
    }
}
