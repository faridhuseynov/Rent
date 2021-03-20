using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rent.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required (ErrorMessage = "NameRequiredMessage")]
        public string Name { get; set; }

        [Required(ErrorMessage = "SurnameRequiredMessage")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "UsernameRequiredMessage")]
        public string Username { get; set; }

        [Required (ErrorMessage = "EmailRequiredMessage")]
        [EmailAddress (ErrorMessage = "EmailFormatMessage")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PasswordRequiredMessage")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPasswordRequired")]
        [Compare("Password",ErrorMessage = "PasswordMatchError")]
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalRegisters { get; set; }
    }
}
