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

        public AgentSeeder(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            if (_context.Agents.Any())
                return;

            var gazprom = _context.Companies.Where(c => c.Name == "Gazprom").First();
            var sber = _context.Companies.Where(c => c.Name == "SBER").First();
            var yandex = _context.Companies.Where(c => c.Name == "Yandex").First();
            var huawei = _context.Companies.Where(c => c.Name == "Huawei").First();

            var companies = new List<Company> { gazprom, sber, yandex };

            var gazpromAgentData = new List<(string, string, string)>
            {
                ("Vladimir", "Belov", "Tech lead"),
                ("Ivan", "Ivanov", "HR manager"),
            };

            var sberAgentsData = new List<(string, string, string)>
            {
                ("Semen", "Karpov", "SCRUM master"),
                ("Dmitry", "Lyadov", "HR manager"),
            };

            var yandexAgentsData = new List<(string, string, string)>
            {
                ("Andrew", "Novikov", "HR lead"),
                ("Mike", "Kravetz", "HR manager"),
            };

            var huaweiAgentsData = new List<(string, string, string)>
            {
                ("Meng", "Tao", "HR"),
            };

            await AddCompanyAgents(gazprom, gazpromAgentData);
            await AddCompanyAgents(sber, sberAgentsData);
            await AddCompanyAgents(yandex, yandexAgentsData);
            await AddCompanyAgents(huawei, huaweiAgentsData);
        }

        private async Task AddCompanyAgents(Company company, List<(string, string, string)> agentsData)
        {
            var password = "Agent123!";

            foreach (var agentData in agentsData)
            {
                var firstName = agentData.Item1;
                var lastName = agentData.Item2;
                var position = agentData.Item3;

                var email = $"{firstName.ToLower()[0]}.{lastName.ToLower()}@{company.Name.ToLower()}";

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
