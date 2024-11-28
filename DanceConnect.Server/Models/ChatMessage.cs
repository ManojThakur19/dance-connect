namespace DanceConnect.Server.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string? Message { get; set; }
        public bool IsDelivered { get; set; }
        public DateTime SentOn { get; set; } = DateTime.Now;
    }
}
