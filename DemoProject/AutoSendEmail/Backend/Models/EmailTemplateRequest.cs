namespace CommonBase.Models
{
    public class BaseEmailRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Subject { get; set; }
        public string MessageBody { get; set; }
    }

    public class EmailTemplateRequest : BaseEmailRequest
    {

        public string AttachmentFilePath { get; set; }
    
    }
}
