using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.SignalR;
using Server_side.connectionManagers;
using Server_side.enums;

namespace Server_side.Hubs
{
    public class DemoHub : Hub
    {
        private readonly connectionManagers.IConnectionManager connectionManager;

        public DemoHub(connectionManagers.IConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }

        public override Task OnConnectedAsync()
        {
            // Lấy thông tin người dùng và connectionId từ Context
            var userId = Context.User.Identity.Name;
            var connectionId = Context.ConnectionId;

            // Lưu lại thông tin connectionId
            connectionManager.AddConnection(userId, connectionId);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            // Lấy thông tin người dùng từ Context
            var userId = Context.User.Identity.Name;

            // Xóa thông tin connectionId khi client ngắt kết nối
            connectionManager.RemoveConnection(userId);

            return base.OnDisconnectedAsync(exception);
        }

        //Các phương thức trong Hub
        public async Task SendMessage(string user, string message, SendModes mode = SendModes.ALL)
        {
            switch (mode)
            {
                case SendModes.ALL:
                    // Gửi tin nhắn cho tất cả
                    await Clients.All.SendAsync("ReceiveMessage", user, message);
                    break;

                case SendModes.CALLER:
                    // Gửi tin nhắn cho người gọi hàm
                    await Clients.Caller.SendAsync("ReceiveMessage", user, message);
                    break;

                case SendModes.OTHER:
                    // Gửi tin nhắn cho tất cả (trừ người gọi hàm)
                    await Clients.Others.SendAsync("ReceiveMessage", user, message);
                    break;

                default: break;
            }
        }
        public async Task SendToUser(string userId, string mesage)
        {
            if (userId == null) return;
            var connectionId = connectionManager.GetConnectionId(userId);
            if (connectionId == null) return;
            await Clients.Client(connectionId).SendAsync("SendToUser", mesage);
        }
    }
}