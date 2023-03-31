using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Domain.Entities;
using BricsSocial.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Infrastructure.Persistence.Seeding
{
    public sealed class VacancySeeder : ISeeder
    {
        private readonly IApplicationDbContext _context;

        private readonly List<Vacancy> _gazpromNeftVacancies = new List<Vacancy>
        {
            new Vacancy
            {
                Name = "International Project Manager",
                Requirements = "Need professional manager for our new international project. 5+ years of experience in project management.",
                Offerings = "3k$ in national currency, free schedule",
                SkillTags = "PM,GR,Project management",
                Status = VacancyStatus.Open,
                CompanyId = 1,
            },
            new Vacancy
            {
                Name = "Tech Lead",
                Requirements = "We're hiring Tech Lead for our new project. International specialists are welcome!",
                Offerings = "7k$ in national currency, remote work",
                SkillTags = "Lead,Tech,Backend",
                Status = VacancyStatus.Open,
                CompanyId = 1,
            },

        };

        public VacancySeeder(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (_context.Vacancies.Any())
                return;

        }
    }
}
