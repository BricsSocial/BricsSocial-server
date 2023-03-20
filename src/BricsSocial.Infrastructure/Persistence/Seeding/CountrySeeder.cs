using BricsSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Infrastructure.Persistence.Seeding
{
    internal class CountrySeeder : ISeeder
    {
        private readonly ApplicationDbContext _context;

        public CountrySeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (_context.Countries.Any())
                return;

            var countries = new List<Country>
            {
                new Country { Name = "Brasil" },
                new Country { Name = "Russia" },
                new Country { Name = "India" },
                new Country { Name = "China" },
                new Country { Name = "South Africa" },
            };

            _context.Countries.AddRange(countries);

            await _context.SaveChangesAsync();
        }
    }
}
