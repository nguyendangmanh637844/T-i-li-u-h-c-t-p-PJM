using Server_side.models;

namespace Server_side.IServices
{
    public interface ISignalRService
    {
        Task SendToAll(SendToAllRequest messageDto);
        Task SendToConnection(string connectionId, string methodName, params object[] args);
        Task<string> GetConnectionIdByUsername(string username);
    }
}
