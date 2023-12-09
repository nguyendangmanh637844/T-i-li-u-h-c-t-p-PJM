namespace Server_side.models
{
    public class MessageDto
    {
        public String User { get; set; }
        public String Message { get; set; }

        public MessageDto(string user, string message)
        {
            User = user;
            Message = message;
        }

        public MessageDto()
        {
        }
    }
}