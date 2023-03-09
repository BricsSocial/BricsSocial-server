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

        public string Name { get; set; }

        public string CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
