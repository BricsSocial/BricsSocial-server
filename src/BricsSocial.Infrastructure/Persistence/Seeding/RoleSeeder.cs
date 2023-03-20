using BricsSocial.Application.Common.Security;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Infrastructure.Persistence.Seeding
{
    internal sealed class RoleSeeder : ISeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleSeeder(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            var administratorRole = new IdentityRole(UserRoles.Administrator);
            var agentRole = new IdentityRole(UserRoles.Agent);
            var specialistRole = new IdentityRole(UserRoles.Specialist);

            if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
                await _roleManager.CreateAsync(administratorRole);
            if (_roleManager.Roles.All(r => r.Name != agentRole.Name))
                await _roleManager.CreateAsync(agentRole);
            if (_roleManager.Roles.All(r => r.Name != specialistRole.Name))
                await _roleManager.CreateAsync(specialistRole);
        }
    }
}
