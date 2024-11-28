using DanceConnect.Server.Models;

namespace DanceConnect.Server.Services
{
    public interface IChatService
    {
        Task<IEnumerable<ChatMessage>> GetQueuedMessagesAsync(int recipientId);
        Task QueueMessageAsync(ChatMessage message);
        Task MarkMessageAsDeliveredAsync(int messageId);
    }
}
