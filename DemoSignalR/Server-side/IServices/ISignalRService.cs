namespace Server_side.IServices
{
    public interface ISignalRService
    {
        Task SendToAll(string methodName, params object[] args);
        Task SendToConnection(string connectionId, string methodName, params object[] args);
        Task<string> GetConnectionIdByUsername(string username);
    }
}
