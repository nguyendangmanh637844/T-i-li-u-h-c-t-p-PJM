using Microsoft.AspNetCore.SignalR;
using Server_side.Hubs;
using Server_side.IServices;
using Server_side.models;

namespace Server_side.Services
{
    public class SignalRService : ISignalRService
    {
        private readonly DemoHub _hubContext;
        private readonly Dictionary<string, string> _usernameConnectionMap;

        public SignalRService(DemoHub hubContext)
        {
            _hubContext = hubContext;
            _usernameConnectionMap = new Dictionary<string, string>();
        }

        public async Task SendToAll(SendToAllRequest messageDto)
        {
            await _hubContext.SendMessage(messageDto);
        }

        public async Task SendToConnection(string connectionId, string methodName, params object[] args)
        {
            await _hubContext.Clients.Client(connectionId).SendAsync(methodName, args);
        }

        public async Task<string> GetConnectionIdByUsername(string username)
        {
            if (_usernameConnectionMap.TryGetValue(username, out var connectionId))
            {
                return connectionId;
            }

            // Trả về null nếu không tìm thấy
            return null;
        }

        // Các hàm phục vụ thêm vào dictionary khi client kết nối
        public void AddConnectionId(string username, string connectionId)
        {
            _usernameConnectionMap[username] = connectionId;
        }

        // Các hàm phục vụ khi client ngắt kết nối
        public void RemoveConnectionId(string username)
        {
            if (_usernameConnectionMap.ContainsKey(username))
            {
                _usernameConnectionMap.Remove(username);
            }
        }
    }
}