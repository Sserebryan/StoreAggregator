using System;
using System.ComponentModel.DataAnnotations;

namespace WEB.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [StringLength(32, ErrorMessage = "The names length must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public String FullName { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }
}
