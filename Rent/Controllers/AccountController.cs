using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
//using System.Web.Providers.Entities;
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
                UserName = newUser.Username
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
            await signInManager.SignInAsync(user, isPersistent: false);
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
            var user = await userManager.FindByNameAsync(User.Username);
            if (user == null)
                return View();

            var result = await userManager.CheckPasswordAsync(user, User.Password);
            if (result!=false)
            {
                await signInManager.SignInAsync(user, result);
                return RedirectToAction("Index", "Home");
                
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> AccountInfo()
        {
            var userFromRepo = await userManager.FindByNameAsync(User.Identity.Name);
            return View(userFromRepo);
        }



        [HttpPost]
        public async Task ChangeProfilePicture(IEnumerable<IFormFile> images)
        {
            int count = 0;
            var userFromRepo = await userManager.FindByNameAsync(User.Identity.Name);
            foreach (var item in images)
            {
                ++count;
            }
            //userFromRepo.MainProfilePicture = formData.Name;
            //var pics = image;
        }

        [HttpPost]
        public async Task<IActionResult> ProfileDetailsChange(string name, string surname,
            string phoneNumber, string meetingLocation, string email, string gender, string country,
            string city)
        {
            var userFromRepo = await userManager.FindByNameAsync(User.Identity.Name);
            if (ModelState.IsValid == true)
            {
                userFromRepo.Name = name;
                userFromRepo.Surname = surname;
                userFromRepo.Email = email;
                userFromRepo.MeetingLocation = meetingLocation;
                userFromRepo.PhoneNumber = phoneNumber;
                userFromRepo.City = city;
                userFromRepo.Country = country;
                userFromRepo.Gender = gender;
                await userManager.UpdateAsync(userFromRepo);
                //await userManager.ChangeEmailAsync(userFromRepo, email,userFromRepo.);
            }
            return RedirectToAction("AccountInfo");
        }

        [HttpPost]
        public async Task<IActionResult> PasswordChange(string password)
        {
            var userFromRepo = await userManager.FindByNameAsync(User.Identity.Name);
            if (ModelState.IsValid == true)
            {
                userFromRepo.PasswordHash= userManager.PasswordHasher.HashPassword(userFromRepo, password);
                await userManager.UpdateAsync(userFromRepo);

            }
            return RedirectToAction("AccountInfo");
        }

        [HttpGet]
        public async Task<IActionResult> Profile(string username)
        {
            if (username != User.Identity.Name)
            {
                var userFromRepo = await userManager.FindByNameAsync(username);
                if (userFromRepo != null)
                {
                    return View(userFromRepo);
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}