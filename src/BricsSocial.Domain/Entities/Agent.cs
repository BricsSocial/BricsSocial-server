using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Domain.Entities
{
    public sealed class Agent : UserBase
    {
        public string Position { get; set; }
        public string? Photo { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public List<Reply> Replies { get; set; } = new List<Reply>();

        public static class Invariants
        {
            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 100;
            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 100;
            public const int PositionMinLength = 1;
            public const int PositionMaxLength = 200;
            public const int PhotoMinLength = 1;
            public const int PhotoMaxLength = 2 << 20;
        }
    }
}
