using System.ComponentModel.DataAnnotations;

namespace WEB.ViewModels.Account
{
    public class EmailVerificationViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        public string StatusMessage { get; set; }
    }
}