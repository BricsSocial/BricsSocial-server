using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Domain.Entities
{
    public sealed class Specialist : BaseEntity
    {
        public string Name { get; set; }

        public string Photo { get; set; }

        public string About { get; set; }
    }
}
