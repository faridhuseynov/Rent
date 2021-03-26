using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
//using System.Web.Providers.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
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
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
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
                        return View("Register",rvm);
                    }
                    await userManager.AddToRoleAsync(user, "User");
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
            {
                newUser.ExternalRegisters = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                return View("Register", newUser);
            }
            var user = new User()
            {
                Name = newUser.Name,
                Surname = newUser.Surname,
                Email = newUser.Email,
                UserName = newUser.Username,
                MainProfilePicture = "avatar.png",

            };

            var result = await userManager.CreateAsync(user, newUser.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                newUser.ExternalRegisters = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                return View("Register", newUser);
            }
            await userManager.AddToRoleAsync(user, "User");

            //this is for the email confirmation for the new user, can be done later
            //var token = userManager.GenerateEmailConfirmationTokenAsync(user);
            //var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);

            //var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            //var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);
            //var message = new Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging.Message(new string[] { user.Email }, "Confirmation email link", confirmationLink, null);
            //await _emailSender.SendEmailAsync(message);

            //await signInManager.SignInAsync(user, isPersistent: false);
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
            User.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (!ModelState.IsValid)
                return View("Login",User);
            // think about adding errors while login failed
            var user = await userManager.FindByNameAsync(User.Username);
            if (user == null || user.IsUserBlocked==true)
                return RedirectToAction("Login",User);

            var result = await userManager.CheckPasswordAsync(user, User.Password);
         
            if (result != false)
            {
                await signInManager.SignInAsync(user, result);
                if (await userManager.IsInRoleAsync(user, "Admin"))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                if (!String.IsNullOrWhiteSpace(User.ReturnUrl) &&
                    User.ReturnUrl != "/Home/SendProposal")
                {
                    return RedirectToAction(User.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");

            }
            //result.Succeeded
            return View("Login",User);
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
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("/");
            LoginViewModel lvm = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View("Login", lvm);
            }

            var info = await signInManager.GetExternalLoginInfoAsync();
            var user = new User();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information");
                return View("Login", lvm);
            }
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            if (email != null)
            {
                user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "User with this email not found in the system");
                    return View("Login", lvm);
                }
                else if (user.IsUserBlocked)
                {
                    ModelState.AddModelError(string.Empty, "User is blocked");
                    return View("Login", lvm);
                }
                else
                {
                    var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: false);

                    if (signInResult.Succeeded)
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        await userManager.AddLoginAsync(user, info);
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
            }
            else
            {
                ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
                ViewBag.ErrorMessage = "Please contact support  on farid@gmail.com";
                return View("Login");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task BlockUnblockUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if ((user != null) && user.Name != User.Identity.Name && !(await userManager.IsInRoleAsync(user, "Admin")))
            {
                if (user.IsUserBlocked == true)
                {
                    user.LockoutEnabled = false;
                    user.LockoutEnd = DateTime.Now;
                }
                else
                {
                    user.LockoutEnabled = true;
                    user.LockoutEnd = DateTime.Now.AddYears(100);
                }
                user.IsUserBlocked = !(user.IsUserBlocked);
                if (user.IsUserBlocked)
                {
                    await userManager.UpdateSecurityStampAsync(user);
                }
                    await userManager.UpdateAsync(user);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> AccountInfo(string userId)
        {
            var userFromRepo = await userManager.FindByNameAsync(User.Identity.Name);
            return View(userFromRepo);
        }


        [HttpPost]
        public async Task<string> ChangeProfilePicture(IFormFile image)
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
                        return ex.Message;
                    }

                    if (fileName != null && fileName!="")
                    {
                        userFromRepo.MainProfilePicture = fileName;
                        await userManager.UpdateAsync(userFromRepo);
                    }
                }
            }
            return fileName;
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
                userFromRepo.PasswordHash = userManager.PasswordHasher.HashPassword(userFromRepo, password);
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