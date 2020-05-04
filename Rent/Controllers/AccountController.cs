﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
//using System.Web.Providers.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rent.DomainModels.Models;
using Rent.ServiceLayers;
using Rent.ViewModels.AccountViewModels;

namespace Rent.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Register(string returnUrl)
        {
            RegisterViewModel rvm = new RegisterViewModel
            {
                ReturnUrl = returnUrl,
                ExternalRegisters = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(rvm);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalRegister(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalRegisterCallback", "Account",
                new { ReturnUrl = returnUrl });

            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalRegisterCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("/");
            RegisterViewModel rvm = new RegisterViewModel
            {
                ReturnUrl = returnUrl,
                ExternalRegisters = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View("Register", rvm);
            }

            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external register information");
                return View("Login", rvm);
            }

            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: false);
            
            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (email != null)
                {
                    var user = new User
                    {
                        UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                    };
                    var result = await userManager.CreateAsync(user);
                    if (!result.Succeeded)
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError(item.Code, item.Description);
                        }
                        return View("Register");
                    }

                        await userManager.AddLoginAsync(user, info);
                        await signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
            }
            return View("Register");
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginViewModel lvm = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(lvm);
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account",
                new { ReturnUrl = returnUrl });

            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult>ExternalLoginCallback(string returnUrl=null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("/");
            LoginViewModel lvm = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            if (remoteError!=null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View("Login", lvm);
            }

            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info==null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information");
                return View("Login", lvm);
            }

            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: false);

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (email!=null)
                {
                    var user = await userManager.FindByEmailAsync(email);
                    if (user==null)
                    {
                        ModelState.AddModelError(string.Empty, "User with this email not found in the system");
                        return View("Login", lvm);
                    }
                    await userManager.AddLoginAsync(user, info);
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
            }

            ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
            ViewBag.ErrorMessage = "Please contact support  on farid@gmail.com";
            return View("Login");
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


        //IEnumerable<IFormFile>
        [HttpPost]
        public async Task<IActionResult> ChangeProfilePicture(IFormFile image)
        {
            var userFromRepo = await userManager.FindByNameAsync(User.Identity.Name);
            string fileName = "";
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    try
                    {
                        fileName = FileUploaderService.UploadUserPhoto(image);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest();
                    }

                    if (fileName != null)
                    {
                        userFromRepo.MainProfilePicture = fileName;
                        await userManager.UpdateAsync(userFromRepo);
                    }
                }
                //userFromRepo.MainProfilePicture = formData.Name;
                //var pics = image;
            }
            return RedirectToAction("AccountInfo");
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