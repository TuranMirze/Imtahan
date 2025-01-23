using Imtahan.Enums;
using Microsoft.AspNetCore.Identity;
using Imtahan.Models;

namespace Imtahan.Extension
{
    public static class SeedExtension
    {
        //public static void UseUserSeed(this IApplicationBuilder app)
        //{
        //    using (var scope = app.ApplicationServices.CreateScope())
        //    {
        //        var UserManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        //        var RoleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        //        if (!RoleManager.Roles.Any())
        //        {
        //            foreach (Roles item in Enum.GetValues(typeof(Roles)))
        //            {
        //                RoleManager.CreateAsync(new IdentityRole(item.ToString())).Wait();
        //            }
        //        }
        //        if (!UserManager.Users.Any(x => x.NormalizedUserName == "ADMIN"))
        //        {
        //            User user = new User()
        //            {
        //                Name = "admin",
        //                UserName = "admin",
        //                Email = "admin@mail.ru",
        //            };
        //            UserManager.CreateAsync(user, "+Aa123*").Wait();
        //            UserManager.AddToRoleAsync(user, nameof(Roles.Admin)).Wait();
        //        }
        //    }

        //}
    }
}
