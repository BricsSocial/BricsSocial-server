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

        public CompanySeeder(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (_context.Companies.Any())
                return;

            var russia = _context.Countries.Where(c => c.Name == "Russia").First();
            var china = _context.Countries.Where(c => c.Name == "China").First();

            var companies = new List<Company>
            {
                new Company { Name = "Gazprom", Description = "Global Oil and Gas company", CountryId = russia.Id },
                new Company { Name = "SBER", Description = "Largest bank and a leading global financial institution with a 179-year history", CountryId = russia.Id },
                new Company { Name = "Yandex", Description = "Multinational technology company providing Internet-related products and services", CountryId = russia.Id },
                new Company { Name = "Huawei", Description = "Leading provider of information and communications technology, infrastructure and smart devices", CountryId = china.Id }
            };

            _context.Companies.AddRange(companies);

            await _context.SaveChangesAsync(default);
        }
    }
}
