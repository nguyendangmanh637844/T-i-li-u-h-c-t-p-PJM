namespace Server_side.connectionManagers
{
    public interface IConnectionManager
    {
        void AddConnection(string userId, string connectionId);

        void RemoveConnection(string userId);

        string GetConnectionId(string userId);
    }
}