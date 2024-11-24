using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace DanceConnect.Server.Entities
{
    public class ContactUs
    {
        public int Id { get; set; }

        [Required, MinLength(5)]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Message { get; set; }

        public string? MessageResponse { get; set; }

        [NotMapped]
        public bool IsMessageResponded
        {
            get => MessageResponse is not null || MessageResponse != string.Empty;
        }

        [NotMapped]
        public string? DisplayMessage
        {
            get => Message?.Length > 10 ? Message.Substring(0, 10) + "..." : Message; 
        }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
