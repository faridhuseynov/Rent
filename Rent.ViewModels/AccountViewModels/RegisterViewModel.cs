using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rent.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required (ErrorMessage ="Ad xanası mütləq doldurulmalıdır")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad xanası mütləq doldurulmalıdır")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "İstifadəçi adı xanası mütləq doldurulmalıdır")]
        public string Username { get; set; }

        [Required (ErrorMessage ="Email adresi mütləqdir")]
        [EmailAddress (ErrorMessage ="Email adresi düzgün formatda deyil")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifrə mütləqdir")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Şifrəni təkrarlamanız mütləqdir")]
        [Compare("Password",ErrorMessage ="İlkin şifrəylə təkrar şifrə uyğun gəlmir")]
        
        public string ConfirmPassword { get; set; }
    }
}
