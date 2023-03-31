using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Infrastructure.Persistence.Seeding
{
    internal class CompanySeeder : ISeeder
    {
        private readonly IApplicationDbContext _context;

        private readonly List<Company> _companies = new List<Company>
        {
            new Company
            {
                Name = "Gazprom Neft",
                Description = "Gazprom Neft PAO is an integrated oil company engaged in the exploration, development, production, transportation, and sale of crude oil and gas.",
                CountryId = 2
            },
            new Company
            {
                Name = "ROSATOM",
                Description = "State Atomic Energy Corporation Rosatom (ROSATOM) is one of global technological leaders, with capacities in the nuclear sector and beyond, and business partners in 50 countries.",
                CountryId = 2
            }
        };

        public CompanySeeder(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (_context.Companies.Any())
                return;

            //_context.Companies.AddRange(_companies);

            //await _context.SaveChangesAsync(default);
        }
    }
}
