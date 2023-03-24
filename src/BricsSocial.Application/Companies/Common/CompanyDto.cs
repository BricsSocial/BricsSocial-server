using BricsSocial.Application.Common.Mappings;
using BricsSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Companies.Common
{
    public sealed class CompanyDto : IMapFrom<Company>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Logo { get; set; }

        public int CountryId { get; set; }
    }
}
