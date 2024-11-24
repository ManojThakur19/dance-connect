using DanceConnect.Server.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanceConnect.Server.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, MinLength(2)]
        public string? Name { get; set; }

        [Required]
        public string? Gender { get; set; }

        public string? ProfilePic { get; set; } 

        public string? IdentityDocument { get; set; } 

        public ProfileStatus ProfileStatus { get; set; }

        [Required]
        public string? Street { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        [RegularExpression(@"^\d+$")]
        public string? PostalCode { get; set; }

        [Required]
        public string? Province { get; set; }

        [Required]
        public DateTime Dob { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$")]
        public string? Phone { get; set; }

        [ForeignKey(nameof(AppUser))]
        public int AppUserId { get; set; }
        public ApplicationUser? AppUser { get; set; }
    }
}
