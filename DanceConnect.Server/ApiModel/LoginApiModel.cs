using System.ComponentModel.DataAnnotations;

namespace DanceConnect.Server.ApiModel
{
    public class LoginApiModel
    {
        [Required]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
