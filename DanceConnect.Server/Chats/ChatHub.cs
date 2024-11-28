using DanceConnect.Server.Entities;
using DanceConnect.Server.Models;
using DanceConnect.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace DanceConnect.Server.Chats
{
    [Authorize]
    public class ChatHub : Hub
    {
        private static readonly ConcurrentDictionary<string, string> _userConnections = new();
        private readonly IChatService chatService;
        private readonly UserManager<ApplicationUser> userManager;

        public ChatHub(IChatService chatService, UserManager<ApplicationUser> userManager)
        {
            this.chatService = chatService;
            this.userManager = userManager;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User.Claims.FirstOrDefault(x=>x.Type == "Id")?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                _userConnections[userId] = Context.ConnectionId;

                // Send any queued messages to the user
                var queuedMessages = await chatService.GetQueuedMessagesAsync(int.Parse(userId));
                foreach (var message in queuedMessages)
                {
                    await Clients.Caller.SendAsync("ReceiveMessage", message);
                    await chatService.MarkMessageAsDeliveredAsync(message.Id);
                }
            }
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (userId != null)
            {
                _userConnections.TryRemove(userId, out _);
            }
            return base.OnDisconnectedAsync(exception);
        }


        public async Task SendMessage(ChatMessage message)
        {
            var senderUserId = Context.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            
            if (string.IsNullOrEmpty(senderUserId) || string.IsNullOrEmpty(message.ReceiverId.ToString()))
            {
                throw new HubException($"Sender does not found!");
            }

            var sender = await userManager.FindByIdAsync(senderUserId);
            var receiver = await userManager.FindByIdAsync(message.ReceiverId.ToString());

            if (_userConnections.TryGetValue(message.ReceiverId.ToString(), out var connectionId))
            {
                // User is online, send message directly
                await Clients.Client(connectionId).SendAsync("ReceiveMessage", new ChatMessage
                {
                    SenderId = int.Parse(senderUserId),
                    ReceiverId = message.ReceiverId,
                    Message = message.Message
                });
            }
            else
            {
                // User is offline, queue the message
                await chatService.QueueMessageAsync(new ChatMessage
                {
                    SenderId = int.Parse(senderUserId),
                    ReceiverId = message.ReceiverId,
                    Message = message.Message
                });
            }
        }
    }
}
