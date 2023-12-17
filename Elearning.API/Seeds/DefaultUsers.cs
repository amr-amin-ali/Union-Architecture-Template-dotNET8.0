using Elearning.Entittes.Models;
using Elearning.Shared.Enums;

using Microsoft.AspNetCore.Identity;

using System.Security.Claims;

namespace Elearning.API.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedBasicUserAsync(UserManager<ApplicationUser> userManager)
        {
            try
            {
                var defaultUser = new ApplicationUser
                {
                    UserName = "basicuser@domain.com",
                    Email = "basicuser@domain.com",
                    EmailConfirmed = true
                };

                var user = await userManager.FindByEmailAsync(defaultUser.Email);

                if (user == null)
                {
                    defaultUser.Image = null;

                    await userManager.CreateAsync(defaultUser, "P@ssword123");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public static async Task SeedSuperAdminUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManger)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin@domain.com",
                Email = "superadmin@domain.com",
                EmailConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(defaultUser.Email);

            if (user == null)
            {
                defaultUser.Image = null;
                await userManager.CreateAsync(defaultUser, "P@ssword123");
                await userManager.AddToRolesAsync(defaultUser, new List<string> { Roles.Basic.ToString(), Roles.Admin.ToString(), Roles.SuperAdmin.ToString() });
            }

            await roleManger.SeedClaimsForSuperUser();
        }

        private static async Task SeedClaimsForSuperUser(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync(Roles.SuperAdmin.ToString());

        }


    }
}