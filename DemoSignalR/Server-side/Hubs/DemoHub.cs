using Microsoft.AspNetCore.SignalR;

namespace Server_side.Hubs
{
    public class DemoHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // Xử lý logic và gửi tin nhắn đến tất cả client
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}