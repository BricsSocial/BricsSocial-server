using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Domain.Entities
{
    public sealed class Specialist : EntityBase
    {
        public string IdentityId { get; set; }

        public string About { get; set; }
        public string Photo { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public int ResumeId { get; set; }
        public Resume Resume { get; set; }

        public List<FriendRequest> FriendRequests { get; set; }
    }
}
