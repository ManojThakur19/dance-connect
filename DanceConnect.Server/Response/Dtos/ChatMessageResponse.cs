namespace DanceConnect.Server.Response.Dtos
{
    public class ChatMessageResponse
    {
        public int SenderId { get; set; }
        public string? SenderName { get; set; }
        public string? SenderImage { get; set; }
        public int ReceiverId { get; set; }
        public string? ReceiverName { get; set; }
        public string? ReceiverImage { get; set; }
        public string? Message { get; set; }
        public string? SentOn { get; set; }
    }
}
