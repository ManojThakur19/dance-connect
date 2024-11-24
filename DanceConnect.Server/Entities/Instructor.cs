using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DanceConnect.Server.Enums;

namespace DanceConnect.Server.Entities
{
    public class Instructor
    {
        [Key]
        public int InstructorId { get; set; }

        [Required, MinLength(10)]
        public string? Name { get; set; }

        [Required]
        public string? Gender { get; set; }
        [Required]
        public DateTime Dob { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$")]
        public string? Phone { get; set; }

        public decimal HourlyRate { get; set; }
        public ProfileStatus ProfileStatus { get; set; }

        public string? ProfilePic { get; set; }

        public string? IdentityDocument { get; set; }

        public string? IntroVideo { get; set; }

        [Required]
        public string? Street { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        [RegularExpression(@"^\d+$")]
        public string? PostalCode { get; set; }

        [Required]
        public string? Province { get; set; }

        [ForeignKey(nameof(AppUser))]
        public int AppUserId { get; set; }
        public ApplicationUser? AppUser { get; set; }

        public ICollection<Rating> Ratings { get; set; } = [];
    }
}
