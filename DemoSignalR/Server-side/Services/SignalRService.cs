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

        public async Task SendToUser(SendToUser request)
        {
            await _hubContext.SendToUser(request);
        }
    }
}