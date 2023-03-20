using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Infrastructure.Persistence.Seeding
{
    internal sealed class SkillTagSeeder : ISeeder
    {
        private readonly IApplicationDbContext _context;

        public SkillTagSeeder(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (_context.SkillTags.Any())
                return;

            var skillTags = new List<SkillTag>
            {
                // Tech
                new SkillTag { Name = "Backend" },
                new SkillTag { Name = "Frontend" },
                new SkillTag { Name = "Mobile" },
                new SkillTag { Name = "iOS" },
                new SkillTag { Name = "Android" },
                new SkillTag { Name = "Machine Learning" },
                new SkillTag { Name = "IoT" },
                new SkillTag { Name = "DevOps" },
                new SkillTag { Name = "QA" },
                new SkillTag { Name = "System analysis" },
                new SkillTag { Name = "Analytics" },

                // Programming languages
                new SkillTag { Name = "C#" },
                new SkillTag { Name = "Flutter" },
                new SkillTag { Name = "JavaScript" },
                new SkillTag { Name = "Swift" },
                new SkillTag { Name = "Java" },
                new SkillTag { Name = "C++" },
                new SkillTag { Name = "Kotlin" },
                new SkillTag { Name = "Python" },
                new SkillTag { Name = "Go" },
                new SkillTag { Name = "PHP" },

                 // Management
                 new SkillTag { Name = "HR" },
                 new SkillTag { Name = "GR" },
                 new SkillTag { Name = "Management" },
                 new SkillTag { Name = "Product management" },
                 new SkillTag { Name = "Project management" },

                 // Science
                 new SkillTag { Name = "R&D" },

                 // Other
                 new SkillTag { Name = "Soft skills" },
            };

            _context.SkillTags.AddRange(skillTags);

            await _context.SaveChangesAsync(default);
        }
    }
}
