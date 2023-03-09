using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Domain.Entities
{
    public sealed class Resume : BaseEntity
    {
        public string SpecialistId { get; set; }
        public string Skills { get; set; }
        public string Experience { get; set; }
    }
}
