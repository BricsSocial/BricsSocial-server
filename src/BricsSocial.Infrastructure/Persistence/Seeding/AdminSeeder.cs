using BricsSocial.Application.Common.Security;
using BricsSocial.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Infrastructure.Persistence.Seeding
{
    internal class AdminSeeder : ISeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminSeeder(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            var administratorUser = new ApplicationUser
            {
                UserName = "admin@brics.org",
                Email = "admin@brics.org"
            };

            if (_userManager.Users.All(u => u.UserName != administratorUser.UserName))
            {
                var createResult = await _userManager.CreateAsync(administratorUser, "Admin123!");
                await _userManager.AddToRolesAsync(administratorUser, new[] { UserRoles.Administrator });
            }
        }
    }
}
