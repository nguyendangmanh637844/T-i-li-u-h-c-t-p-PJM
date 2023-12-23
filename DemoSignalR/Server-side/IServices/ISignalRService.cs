using Server_side.models;

namespace Server_side.IServices
{
    public interface ISignalRService
    {
        Task SendToAll(SendToAllRequest request);

        Task SendToUser(SendToUser request);

    }
}