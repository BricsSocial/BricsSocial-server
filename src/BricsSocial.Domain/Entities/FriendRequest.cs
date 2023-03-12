using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Domain.Entities
{
    public sealed class FriendRequest : EntityBase
    {
        public string Message { get; set; }

        public int FromSpecialistId { get; set; }
        public Specialist FromSpecialist { get; set; }

        public int ToSpecialistId { get; set; }
        public Specialist ToSpecialist { get; set; }
    }
}
