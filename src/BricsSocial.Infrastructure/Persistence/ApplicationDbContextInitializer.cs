using BricsSocial.Application.Common.Security;
using BricsSocial.Domain.Entities;
using BricsSocial.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Infrastructure.Persistence
{
    public class ApplicationDbContextInitializer
    {
        private readonly ILogger<ApplicationDbContextInitializer> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer() || _context.Database.IsSqlite())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            await SeedRolesAsync();

            await SeedAdminAsync();

            await SeedCountriesAsync();

            await SeedCompaniesAsync();
        }

        public async Task SeedRolesAsync()
        {
            // Default roles
            var administratorRole = new IdentityRole(UserRoles.Administrator);
            var agentRole = new IdentityRole(UserRoles.Agent);

            if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
                await _roleManager.CreateAsync(administratorRole);
            if (_roleManager.Roles.All(r => r.Name != agentRole.Name))
                await _roleManager.CreateAsync(agentRole);
        }

        public async Task SeedAdminAsync()
        {
            // Default users
            var administratorUser = new ApplicationUser
            {
                UserName = "admin@brics",
                Email = "admin@brics"
            };

            if (_userManager.Users.All(u => u.UserName != administratorUser.UserName))
            {
                await _userManager.CreateAsync(administratorUser, "Admin123");
                await _userManager.AddToRolesAsync(administratorUser, new[] { UserRoles.Administrator });
            }
        }

        public async Task SeedCountriesAsync()
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

        public async Task SeedCompaniesAsync()
        {
            if (_context.Companies.Any())
                return;

            var country = _context.Countries.Where(c => c.Name == "Russia").First();

            var companies = new List<Company>
            {
                new Company { Name = "Gazprom", Description = "Leading Oil and Gas company" }
            };

            _context.Companies.AddRange(companies);

            await _context.SaveChangesAsync();
        }

        public async Task SeedAgentsAsync()
        {
            var company = _context.Companies.Where(c => c.Name == "Gazprom").First();

            var agentUser = new ApplicationUser
            {
                UserName = "agent@gazprom",
                Email = "agent@gazprom",
                FirstName = "Ivan",
                LastName = "Ivanov",
            };
            if (_userManager.Users.All(u => u.Email != agentUser.Email))
            {
                await _userManager.CreateAsync(agentUser, "agent123");
                await _userManager.AddToRolesAsync(agentUser, new[] { UserRoles.Agent });
            }

            var createdAgentUser = _userManager.Users.Where(u => u.Email == agentUser.Email).First();
            var agent = new Agent
            {
                IdentityId = createdAgentUser.Id,
                Position = "HR manager"
            };
        }
    }
}
