using BricsSocial.Application.Common.Mappings;
using BricsSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Countries.Common
{
    public sealed class CountryDto : IMapFrom<Country>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
