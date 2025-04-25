using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Web.Models
{
    public class RegistrationModel
    {
        [Required, EmailAddress, Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required, Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required, StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6), DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Display(Name = "Confirm password"), Compare("Password", ErrorMessage = "The password and confirm password do not match.")]
        public string ConfirmPassword { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
