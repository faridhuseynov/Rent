using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rent.ViewModels.AccountViewModels
{
    
    public class LoginViewModel
    {
        [Required(ErrorMessage = "UsernameRequiredMessage")]
        public string Username { get; set; }
        [Required(ErrorMessage = "PasswordRequiredMessage")]
        //[RegularExpression("^(?=.{8})(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9])", ErrorMessage = "PasswordFormatError")]
        //[RegularExpression(@"^(?=.{8})(?=.*?[\!\#\$\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\]\^\_\`\{\|\}\~])(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9])"
        //    , ErrorMessage ="PasswordFormatError")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
