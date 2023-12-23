using Server_side.enums;

namespace Server_side.models
{
    public class SendToUser
    {
        public string UserId { get; set; }
        public string Message { get; set; }
        public Channels Channel { get; set; }
    }
}