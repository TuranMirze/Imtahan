using Imtahan.DataAccess;
using Imtahan.Extension;
using Imtahan.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Imtahan
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AppDbContext>(x =>
            {
                x.UseSqlServer(builder.Configuration.GetConnectionString("MSSql"));
            });
            // Add services to the container.
            builder.Services.AddIdentity<User, IdentityRole>(x =>
            {
                x.SignIn.RequireConfirmedEmail = true;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireDigit = true;
                x.Password.RequireLowercase = true;
                x.Password.RequireUppercase = true;
                x.Lockout.MaxFailedAccessAttempts = 20;
                x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

            }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseUserSeed();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
           );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Auth}/{action=Login}/{id?}");
            app.Run();
        }
    }
}

