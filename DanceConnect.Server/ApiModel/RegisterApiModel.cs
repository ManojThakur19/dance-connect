using DanceConnect.Server.Entities;
using System.ComponentModel.DataAnnotations;

namespace DanceConnect.Server.ApiModel
{
    public class RegisterApiModel
    {
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage ="Please provide valid email address")]
        public required string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public required string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public required string ConfirmPassword { get; set; }
        public UserType? UserType { get; set; }
    }
}
