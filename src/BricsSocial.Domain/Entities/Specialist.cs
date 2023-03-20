using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Domain.Entities
{
    public sealed class Specialist : UserBase
    {
        public string? ShortBio { get; set; }
        public string? LongBio { get; set; }
        public string? Photo { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public int? ResumeId { get; set; }
        public Resume? Resume { get; set; }

        public List<FriendRequest> FromFriendRequests { get; set; } = new List<FriendRequest>();
        public List<FriendRequest> ToFriendRequests { get; set; } = new List<FriendRequest>();

        public static class Invariants
        {
            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 100;
            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 100;
            public const int ShortBioMinLength = 1;
            public const int ShortBioMaxLength = 70;
            public const int LongBioMinLength = 1;
            public const int LongBioMaxLength = 3000;
            public const int PhotoMinLength = 1;
            public const int PhotoMaxLength = 2 << 20;
        }
    }
}
