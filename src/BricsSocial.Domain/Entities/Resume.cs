using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Domain.Entities
{
    public sealed class Resume : EntityBase
    {
        public string Skills { get; set; }
        public string Experience { get; set; }

        public int SpecialistId { get; set; }
        public Specialist Specialist { get; set; }

        public List<ResumeReplyFeedback> Feedbacks { get; set; }
    }
}
