using Server_side.enums;

namespace Server_side.models
{
    public class SendToAllRequest
    {
        public String User { get; set; }
        public String Message { get; set; }
        public SendModes SendModes { get; set; }
        public Channels Channels { get; set; }

        public SendToAllRequest(string user, string message, SendModes sendModes, Channels channels)
        {
            User = user;
            Message = message;
            SendModes = sendModes;
            Channels = channels;
        }
        public SendToAllRequest() { }
    }
}