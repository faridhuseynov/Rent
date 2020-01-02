using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rent.DomainModels.Models;
using Rent.ViewModels.AccountViewModels;

namespace Rent.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager
            /*RoleManager<IdentityRole> roleManager*/)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel newUser)
        {
            if (!ModelState.IsValid)
                return View();
            var user = new User()
            {
                Name=newUser.Name,
                Surname=newUser.Surname,
                Email = newUser.Email,
                UserName = newUser.Username,
            };

            var result = await userManager.CreateAsync(user, newUser.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel User)
        {
            if (!ModelState.IsValid)
                return View();
            var user = await userManager.FindByEmailAsync(User.Email);

            if (user == null)
                return View();

            await signInManager.SignInAsync(user, true);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}