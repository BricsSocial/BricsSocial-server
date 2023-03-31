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
    internal class AgentSeeder : ISeeder
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly List<Agent> _gazpromNeftAgents = new List<Agent>
        {
            new Agent
            {
                FirstName = "Ivan",
                LastName = "Semenov",
                Position = "HR manager"
            }
        };

        private readonly List<Agent> _rosatomAgents = new List<Agent>
        {
            new Agent
            {
                FirstName = "Vladimir",
                LastName = "Petrov",
                Position = "HR manager"
            }
        };

        public AgentSeeder(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            if (_context.Agents.Any())
                return;

            var gazpromNeft = _context.Companies.Where(c => c.Id == 1).First();
            var rosatom = _context.Companies.Where(c => c.Id == 2).First();

            await AddCompanyAgents(gazpromNeft, _gazpromNeftAgents);
            await AddCompanyAgents(rosatom, _rosatomAgents);
        }

        private async Task AddCompanyAgents(Company company, List<Agent> agentsData)
        {
            var password = "Agent123!";

            foreach (var agentData in agentsData)
            {
                var firstName = agentData.FirstName;
                var lastName = agentData.LastName;
                var position = agentData.Position;

                var email = $"{firstName.ToLower().First()}.{lastName.ToLower()}@{company.Name.Split(" ").First().ToLower()}.ru";

                var agentUser = new ApplicationUser
                {
                    UserName = email,
                    Email = email
                };
                if (_userManager.Users.All(u => u.Email != agentUser.Email))
                {
                    await _userManager.CreateAsync(agentUser, password);
                    await _userManager.AddToRolesAsync(agentUser, new[] { UserRoles.Agent });
                }

                var createdAgentUser = _userManager.Users.Where(u => u.Email == agentUser.Email).First();
                var agent = new Agent
                {
                    Email = agentUser.Email,
                    FirstName = firstName,
                    LastName = lastName,
                    IdentityId = createdAgentUser.Id,
                    Position = position,
                    CompanyId = company.Id
                };

                _context.Agents.Add(agent);
                await _context.SaveChangesAsync(default);
            }
        }
    }
}
