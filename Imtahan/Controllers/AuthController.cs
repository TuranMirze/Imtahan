using Imtahan.DataAccess;
using Imtahan.Enums;
using Imtahan.Models;
using Imtahan.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Imtahan.Controllers
{
    public class AuthController(AppDbContext _context, UserManager<User> _u, SignInManager<User> _s) : Controller
    {
        bool isAuthenticated => User?.Identity.IsAuthenticated ?? false;
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _s.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            if (isAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (isAuthenticated) return RedirectToAction("Index", "Home");
            User user = new User()
            {
                Name = vm.Name,
                Email = vm.Email,
                UserName = vm.UserName,
            };
            var result = await _u.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            var RoleResult = await _u.AddToRoleAsync(user, nameof(Roles.User));
            if (!RoleResult.Succeeded)
            {
                foreach (var error in RoleResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            return RedirectToAction(nameof(Login));
        }


        [HttpGet]
        public async Task<IActionResult> Login()
        {
            //if (isAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm, string? ReturnUrl = null)
        {
            if (isAuthenticated) return RedirectToAction("Index", "Home");
            User? user = null;
            if (!ModelState.IsValid) return View();
            if (vm.UserNameOrEmail.Contains('@'))
            {
                user = await _u.FindByEmailAsync(vm.UserNameOrEmail);
            }
            else
            {
                user = await _u.FindByNameAsync(vm.UserNameOrEmail);
            }
            if (user is null)
            {
                ModelState.AddModelError("", "Username or Password is wrong");
                return View();
            }

            var result = await _s.PasswordSignInAsync(user, vm.Password, vm.RememberMe, true);
            if (!result.Succeeded)
            {
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "vcvv");
                }
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Wait until" + user.LockoutEnd!.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
            if (string.IsNullOrEmpty(ReturnUrl))
            {
                if (await _u.IsInRoleAsync(user, "Admin"))
                {
                    return RedirectToAction("Index", new { Controller = "Dashboard", Area = "Admin" });
                }
                return RedirectToAction("Index", "Home");

            }
            return LocalRedirect(ReturnUrl);
        }
    }
}
