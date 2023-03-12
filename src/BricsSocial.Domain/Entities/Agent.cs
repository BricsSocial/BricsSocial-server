using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Domain.Entities
{
    public sealed class Agent : EntityBase
    {
        public string IdentityId { get; set; }

        public string Position { get; set; }
        public string Photo { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
