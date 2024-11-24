using System.ComponentModel.DataAnnotations;

namespace DanceConnect.Server.Response.Dtos
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Gender { get; set; }
        public DateTime Dob { get; set; }

        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? ProfileStatus { get; set; }

        public string? ProfilePic { get; set; }
        public string? IdentityDocument { get; set; }

        public string? Street { get; set; }
        public string? PostalCode { get; set; }

        public string? City { get; set; }

        public string? Province { get; set; }
    }
}
