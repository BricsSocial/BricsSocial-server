using BricsSocial.Application.Common.Security;
using BricsSocial.Domain.Entities;
using BricsSocial.Infrastructure.Identity;
using BricsSocial.Infrastructure.Persistence.Seeding;
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

        private readonly IEnumerable<ISeeder> _seeders;

        public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;

            _seeders = new List<ISeeder>
            {
                new RoleSeeder(_roleManager),
                new AdminSeeder(_userManager),
                new CountrySeeder(_context),
                new SkillTagSeeder(_context),

                new CompanySeeder(_context),
                new AgentSeeder(_context, _userManager)
            };
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
                foreach(var seeder in _seeders)
                {
                    await seeder.SeedAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        //public async Task TrySeedAsync()
        //{
        //    await SeedRolesAsync();

        //    await SeedAdminAsync();

        //    await SeedCountriesAsync();

        //    await SeedCompaniesAsync();

        //    await SeedAgentsAsync();
        //}

        //public async Task SeedRolesAsync()
        //{
        //    // Default roles
        //    var administratorRole = new IdentityRole(UserRoles.Administrator);
        //    var agentRole = new IdentityRole(UserRoles.Agent);
        //    var specialistRole = new IdentityRole(UserRoles.Specialist);

        //    if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        //        await _roleManager.CreateAsync(administratorRole);
        //    if (_roleManager.Roles.All(r => r.Name != agentRole.Name))
        //        await _roleManager.CreateAsync(agentRole);
        //    if (_roleManager.Roles.All(r => r.Name != specialistRole.Name))
        //        await _roleManager.CreateAsync(specialistRole);
        //}

        //public async Task SeedAdminAsync()
        //{
        //    // Default users
        //    var administratorUser = new ApplicationUser
        //    {
        //        UserName = "admin@brics",
        //        Email = "admin@brics"
        //    };

        //    if (_userManager.Users.All(u => u.UserName != administratorUser.UserName))
        //    {
        //        var createResult = await _userManager.CreateAsync(administratorUser, "Admin123!");
        //        await _userManager.AddToRolesAsync(administratorUser, new[] { UserRoles.Administrator });
        //    }
        //}

        //public async Task SeedCountriesAsync()
        //{
        //    if (_context.Countries.Any())
        //        return;
            
        //    var countries = new List<Country>
        //    {
        //        new Country { Name = "Brasil" },
        //        new Country { Name = "Russia" },
        //        new Country { Name = "India" },
        //        new Country { Name = "China" },
        //        new Country { Name = "South Africa" },
        //    };

        //    _context.Countries.AddRange(countries);

        //    await _context.SaveChangesAsync();
        //}

        //public async Task SeedCompaniesAsync()
        //{
        //    if (_context.Companies.Any())
        //        return;

        //    var country = _context.Countries.Where(c => c.Name == "Russia").First();

        //    var companies = new List<Company>
        //    {
        //        new Company { Name = "Gazprom", Description = "Leading Oil and Gas company", CountryId = country.Id }
        //    };

        //    _context.Companies.AddRange(companies);

        //    await _context.SaveChangesAsync();
        //}

        //public async Task SeedAgentsAsync()
        //{
        //    if (_context.Agents.Any())
        //        return;

        //    var company = _context.Companies.Where(c => c.Name == "Gazprom").First();

        //    var agentUser = new ApplicationUser
        //    {
        //        UserName = "agent@gazprom",
        //        Email = "agent@gazprom"
        //    };
        //    if (_userManager.Users.All(u => u.Email != agentUser.Email))
        //    {
        //        await _userManager.CreateAsync(agentUser, "Agent123!");
        //        await _userManager.AddToRolesAsync(agentUser, new[] { UserRoles.Agent });
        //    }

        //    var createdAgentUser = _userManager.Users.Where(u => u.Email == agentUser.Email).First();
        //    var agent = new Agent
        //    {
        //        Email = agentUser.Email,
        //        FirstName = "Ivan",
        //        LastName = "Ivanov",
        //        IdentityId = createdAgentUser.Id,
        //        Position = "HR manager",
        //        CompanyId = company.Id
        //    };

        //    _context.Agents.Add(agent);
        //    await _context.SaveChangesAsync();
        //}
    }
}
