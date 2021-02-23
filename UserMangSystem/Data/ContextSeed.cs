using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UserMangSystem.Models;

namespace UserMangSystem.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Basic.ToString()));
        }

        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            Debug.WriteLine("Starting");
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "malshikay95@gmail.com",
                FirstName = "Yohan",
                LastName = "Malshika",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            Debug.WriteLine("Started");
            if (userManager.Users.All(u => u.Id != defaultUser.Id) || defaultUser.Id == null)
            {
                Debug.WriteLine("Entered");
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    Debug.WriteLine("Printed");
                    await userManager.CreateAsync(defaultUser, "Yohan@123");
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.SuperAdmin.ToString());
                }

            }
        }
    }
}
