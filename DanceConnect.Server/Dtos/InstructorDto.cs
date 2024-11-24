using System.ComponentModel.DataAnnotations;

namespace DanceConnect.Server.Dtos
{
    public class InstructorDto
    {
        [Required]
        public string? Name { get; set; }

        public string? Gender { get; set; }
        [Required]
        public DateTime Dob { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$")]
        public string? Phone { get; set; }

        public decimal HourlyRate { get; set; }
        public string? Availability { get; set; }

        public IFormFile? ProfilePic { get; set; }
        public IFormFile? IdentityDocument { get; set; }
        public IFormFile? ShortIntroVideo { get; set; }

        public string? Street { get; set; }

        [Required]
        [RegularExpression(@"^\d+$")]
        public string? PostalCode { get; set; }

        public string? City { get; set; }

        public string? Province { get; set; }
    }
}
