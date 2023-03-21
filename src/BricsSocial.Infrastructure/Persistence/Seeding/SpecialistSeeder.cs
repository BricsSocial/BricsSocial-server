using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Models;
using BricsSocial.Application.Common.Security;
using BricsSocial.Domain.Entities;
using BricsSocial.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Infrastructure.Persistence.Seeding
{
    public sealed class SpecialistSeeder : ISeeder
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SpecialistSeeder(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            if (_context.Specialists.Any())
                return;

            var russia = _context.Countries.Where(c => c.Name == "Russia").First();

            var russiaSpecialistsData = new List<Specialist>
            {
                new Specialist
                {
                    FirstName = "Nikolay",
                    LastName = "Krylov",
                    Bio = "Backend developer, 35 y.o.",
                    Skills =  $"I'm very skilled at Spring, Web development, Microservices.{Environment.NewLine}Also I like developing IoT projects as a hobby",
                    Experience = $"Senior Java dev at Dodo (4 years){Environment.NewLine}Middle Java dev at startups (3 years){Environment.NewLine}Overall more than 10 years",
                    SkillTags = new List<SkillTag> { new SkillTag { Name = "Java" }, new SkillTag { Name = "Backend" }, new SkillTag { Name = "IoT" } }
                },

                new Specialist
                {
                    FirstName = "Sergei",
                    LastName = "Filatov",
                    Bio = "Project manager, Moscow",
                    Skills =  $"Working as a project manager in large Russian companies. Prefer R&D projects management. Experinced in most of modern frameworks and methodologies like SCRUM, Agile, etc.",
                    Experience = $"Project manager at Skolkovo (3 years){Environment.NewLine}SCRUM master at SBER (1 year)",
                    SkillTags = new List<SkillTag> { new SkillTag { Name = "Project management" }, new SkillTag { Name = "R&D" } }
                },

                new Specialist
                {
                    FirstName = "Lev",
                    LastName = "Smirnenko",
                    Bio = "Life enjoyer, freelancer"
                }
            };

            await AddCountrySpecialists(russia, russiaSpecialistsData);
        }

        private async Task AddCountrySpecialists(Country country, List<Specialist> specialistsData)
        {
            var password = "Spec123!";

            foreach (var spec in specialistsData)
            {
                var email = $"{spec.FirstName.ToLower()[0]}.{spec.LastName.ToLower()}@yandex.ru";

                var agentUser = new ApplicationUser
                {
                    UserName = email,
                    Email = email
                };
                if (_userManager.Users.All(u => u.Email != agentUser.Email))
                {
                    await _userManager.CreateAsync(agentUser, password);
                    await _userManager.AddToRolesAsync(agentUser, new[] { UserRoles.Specialist });
                }

                var createdAgentUser = _userManager.Users.Where(u => u.Email == agentUser.Email).First();
                var specialist = new Specialist
                {
                    Email = agentUser.Email,
                    FirstName = spec.FirstName,
                    LastName = spec.LastName,
                    IdentityId = createdAgentUser.Id,
                    Bio = spec.Bio,
                    Skills = spec.Skills,
                    Experience = spec.Experience,
                    CountryId = country.Id
                };

                var tmpTagsSet = spec.SkillTags.Select(t => t.Name).ToHashSet();
                var skillTags = await _context.SkillTags.Where(s => tmpTagsSet.Contains(s.Name)).ToListAsync();
                specialist.SkillTags = skillTags;

                _context.Specialists.Add(specialist);
                await _context.SaveChangesAsync(default);
            }
        }
    }
}
