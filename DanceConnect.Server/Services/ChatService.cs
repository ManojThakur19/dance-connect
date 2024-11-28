using DanceConnect.Server.DataContext;
using DanceConnect.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace DanceConnect.Server.Services
{
    public class ChatService : IChatService
    {
        private readonly DanceConnectContext _context;

        public ChatService(DanceConnectContext context)
        {
            _context = context;
        }
        // Retrieve queued messages for a specific user
        public async Task<IEnumerable<ChatMessage>> GetQueuedMessagesAsync(int recipientId)
        {
            return await _context.Messages
                .Where(m => m.ReceiverId == recipientId && !m.IsDelivered)
                .OrderBy(m => m.SentOn)
                .ToListAsync();
        }

        // Queue a new message
        public async Task QueueMessageAsync(ChatMessage message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        // Mark a message as delivered
        public async Task MarkMessageAsDeliveredAsync(int messageId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message != null)
            {
                message.IsDelivered = true;
                _context.Messages.Update(message);
                await _context.SaveChangesAsync();
            }
        }

    }
}
