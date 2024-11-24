namespace DanceConnect.Server.Models
{
    public class MessageReply
    {
        public required int ReplyTo { get; set; }
        public string? Message { get; set; }
    }
}
