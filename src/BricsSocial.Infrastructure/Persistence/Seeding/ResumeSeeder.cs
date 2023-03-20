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
    internal sealed class ResumeSeeder : ISeeder
    {
        private readonly IApplicationDbContext _context;

        public ResumeSeeder(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (_context.Resumes.Any())
                return;

            var spec1 = await _context.Specialists.Where(s => s.Email == "n.krylov@yandex.ru").FirstAsync();
            var spec2 = await _context.Specialists.Where(s => s.Email == "s.filatov@yandex.ru").FirstAsync();

            var resumesData = new List<(Specialist, string, string, List<string>)>
            {
                (spec1,
                 $"I'm very skilled at Spring, Web development, Microservices.{Environment.NewLine}Also I like developing IoT projects as a hobby",
                 $"Senior Java dev at Dodo (4 years){Environment.NewLine}Middle Java dev at startups (3 years){Environment.NewLine}Overall more than 10 years",
                 new List<string>{ "Java", "Backend", "IoT" }),

                (spec2,
                 $"Working as a project manager in large Russian companies. Prefer R&D projects management. Experinced in most of modern frameworks and methodologies like SCRUM, Agile, etc.",
                 $"Project manager at Skolkovo (3 years){Environment.NewLine}SCRUM master at SBER (1 year)",
                 new List<string>{ "Project management", "R&D" })
            };

            foreach(var resumeData in resumesData)
            {
                var specialist = resumeData.Item1;
                var skills = resumeData.Item2;
                var experience = resumeData.Item3;
                var tags = resumeData.Item4;

                var resume = new Resume();
                resume.Skills = skills;
                resume.Experience = experience;
                resume.SpecialistId = specialist.Id;

                var skillTags = await _context.SkillTags.Where(s => tags.Contains(s.Name)).ToListAsync();
                resume.SkillTags = skillTags;

                _context.Resumes.Add(resume);

                await _context.SaveChangesAsync(default);
            }
        }
    }
}
