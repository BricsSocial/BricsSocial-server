using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using BricsSocial.Domain.Entities;
using BricsSocial.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var russiaSpecialistsData = new List<(string, string, string, string)>
            {
                ("Nikolay", "Krylov", "Backend developer", "Hello! I'm Java developer from Moscow, looking for international projects."),
                ("Sergei", "Filatov", "Project manager", "Very interested in IT project management."),
                ("Lev", "Smirnenko", "Life enjoyer", "Looking for interesting people!))"),
            };

            await AddCountrySpecialists(russia, russiaSpecialistsData);
        }

        private async Task AddCountrySpecialists(Country country, List<(string, string, string, string)> specialistsData)
        {
            var password = "Spec123!";

            foreach (var specialistData in specialistsData)
            {
                var firstName = specialistData.Item1;
                var lastName = specialistData.Item2;
                var shortBio = specialistData.Item3;
                var longBio = specialistData.Item4;

                var email = $"{firstName.ToLower()[0]}.{lastName.ToLower()}@yandex.ru";

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
                    FirstName = firstName,
                    LastName = lastName,
                    IdentityId = createdAgentUser.Id,
                    ShortBio = shortBio,
                    LongBio = longBio,
                    CountryId = country.Id
                };

                _context.Specialists.Add(specialist);
                await _context.SaveChangesAsync(default);
            }
        }
    }
}
