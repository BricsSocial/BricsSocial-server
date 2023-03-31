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

        private readonly List<Specialist> _russiaSpecialists = new List<Specialist>
        {
            new Specialist
            {
                FirstName = "Nikolay",
                LastName = "Krylov",
                Bio = "Backend developer, 35 y.o.",
                About = $"I'm very skilled at Spring, Web development, Microservices.{Environment.NewLine}Also I like developing IoT projects as a hobby.{Environment.NewLine}Senior Java dev at Dodo (4 years){Environment.NewLine}Middle Java dev at startups (3 years){Environment.NewLine}Overall more than 10 years",
                SkillTags = "Java,Backend,IoT,Developer",
                CountryId = 2,
            },
            new Specialist
            {
                FirstName = "Sergei",
                LastName = "Filatov",
                Bio = "Project manager, Moscow",
                About = $"Working as a project manager in large Russian companies. Prefer R&D projects management. Experinced in most of modern frameworks and methodologies like SCRUM, Agile, etc.{Environment.NewLine}Project manager at Skolkovo (3 years){Environment.NewLine}SCRUM master at SBER (1 year)",
                SkillTags = "PM,Project management,R&D",
                CountryId = 2,
            },
            new Specialist
            {
                FirstName = "Lev",
                LastName = "Smirnenko",
                Bio = "GR manager",
                About = "Lead GR manager with 10+ years of experience",
                SkillTags = "GR",
                CountryId = 2,
            }
        };

        private readonly List<Specialist> _indianSpecialists = new List<Specialist>
        {
             new Specialist
            {
                FirstName = "Pratik",
                LastName = "Chada",
                Bio = "GR specialist, Mumbai",
                About = $"I can help your company to make business with India",
                SkillTags = "GR,Business,PM",
                CountryId = 3,
            },
            new Specialist
            {
                FirstName = "Vishal",
                LastName = "Mukherjee",
                Bio = "Project manager, Delhi",
                About = $"Hi! I'm project manager in large Indian companies. Looking for international experience",
                SkillTags = "PM,Management",
                CountryId = 3,
            },
            new Specialist
            {
                FirstName = "Vipul",
                LastName = "Dewan",
                Bio = "Software Developer",
                About = "I'm working as a lead software developer",
                SkillTags = "Lead,Developer,Tech,Java",
                CountryId = 3,
            }
        };


        public SpecialistSeeder(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            if (_context.Specialists.Any())
                return;

            //await AddCountrySpecialists(_russiaSpecialists);
            //await AddCountrySpecialists(_indianSpecialists);
        }

        private async Task AddCountrySpecialists(List<Specialist> specialistsData)
        {
            var password = "Spec123!";

            foreach (var spec in specialistsData)
            {
                var email = $"{spec.FirstName.ToLower().First()}.{spec.LastName.ToLower()}@mail.com";

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
                    About = spec.About,
                    SkillTags = spec.SkillTags,
                    CountryId = spec.CountryId
                };

                _context.Specialists.Add(specialist);
                await _context.SaveChangesAsync(default);
            }
        }
    }
}
