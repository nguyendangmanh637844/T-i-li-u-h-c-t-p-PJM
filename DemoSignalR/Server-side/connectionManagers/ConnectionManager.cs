namespace Server_side.connectionManagers
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly Dictionary<string, string> userConnectionMap = new Dictionary<string, string>();

        public void AddConnection(string userId, string connectionId)
        {
            if (userId == null || connectionId == null) return;
            // Kiểm tra xem userId đã tồn tại chưa, nếu có thì cập nhật connectionId
            if (userConnectionMap.ContainsKey(userId))
            {
                userConnectionMap[userId] = connectionId;
            }
            else
            {
                // Nếu chưa tồn tại thì thêm mới
                userConnectionMap.Add(userId, connectionId);
            }
        }

        public void RemoveConnection(string userId)
        {
            if (userId == null) return;

            // Xóa userId khỏi ánh xạ khi client ngắt kết nối
            if (userConnectionMap.ContainsKey(userId))
            {
                userConnectionMap.Remove(userId);
            }
        }

        public string GetConnectionId(string userId)
        {
            if (userId == null) return null;

            // Lấy ConnectionId dựa trên userId
            if (userConnectionMap.TryGetValue(userId, out var connectionId))
            {
                return connectionId;
            }
            return null; // Trả về null nếu không tìm thấy
        }
    }
}