using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Domain.Entities
{
    public sealed class Company : BaseEntity
    {
        public string Name { get; private set; }

        public string Description { get; private set; }
    }
}
