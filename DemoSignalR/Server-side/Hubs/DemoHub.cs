using Microsoft.AspNetCore.SignalR;
using Server_side.enums;
using Server_side.models;

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
        public async Task SendMessage(SendToAllRequest request)
        {
            // Đảm bảo rằng user và message không bao giờ là null
            string user = request.User ?? string.Empty;
            string message = request.Message ?? string.Empty;

            // Sử dụng GetValueOrDefault để xác định mode và tránh giá trị null
            SendModes mode = request.SendModes == null ? SendModes.ALL : request.SendModes;

            // Sử dụng ToString() trực tiếp mà không cần kiểm tra null
            Channels channel = request.Channels == null ? Channels.CHANNEL_1 : request.Channels;

            switch (mode)
            {
                case SendModes.ALL:
                    // Gửi tin nhắn cho tất cả
                    await Clients.All.SendAsync(channel.ToString(), user, message);
                    break;

                case SendModes.CALLER:
                    // Gửi tin nhắn cho người gọi hàm
                    await Clients.Caller.SendAsync(channel.ToString(), user, message);
                    break;

                case SendModes.OTHER:
                    // Gửi tin nhắn cho tất cả (trừ người gọi hàm)
                    await Clients.Others.SendAsync(channel.ToString(), user, message);
                    break;

                default:
                    // Nếu có thêm các trường hợp khác, xử lý tại đây
                    break;
            }
        }

        public async Task SendToUser(SendToUser request)
        {
            String userId = request.UserId ?? string.Empty;
            if (String.IsNullOrEmpty(userId)) return;
            var connectionId = connectionManager.GetConnectionId(userId);
            if (connectionId == null) return;
            await Clients.Client(connectionId).SendAsync(request.Channel.ToString() ?? Channels.CHANNEL_1.ToString(), request.Message);
        }
    }
}