using DanceConnect.Server.Entities;

namespace DanceConnect.Server.Models
{
    public class EmailMessage
    {
        public required int Sender { get; set; }
        public User? SendingUser { get; set; }

        public required int Receiver { get; set; }
        public Instructor? ReceivingUser { get; set; }

        public required string? Message { get; set; }
    }
}
