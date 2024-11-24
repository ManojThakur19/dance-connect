using System.ComponentModel.DataAnnotations;

namespace DanceConnect.Server.Response.Dtos
{
    public class InstructorResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Gender { get; set; }
        public DateTime Dob { get; set; }

        public string? Phone { get; set; }
        public string? Email { get; set; }

        public decimal HourlyRate { get; set; }
        public decimal AverageRating { get; set; }
        public string? Availability { get; set; }
        public  string? ProfileStatus { get; set; }

        public string? ProfilePic { get; set; }
        public string? IdentityDocument { get; set; }
        public string? ShortIntroVideo { get; set; }

        public string? Street { get; set; }

        public string? PostalCode { get; set; }

        public string? City { get; set; }

        public string? Province { get; set; }
    }
}
