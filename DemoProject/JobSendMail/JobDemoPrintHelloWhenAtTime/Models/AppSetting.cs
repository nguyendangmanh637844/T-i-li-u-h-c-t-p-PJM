namespace BackgroundJobExample.Models
{
    public class AppSetting
    {
        public string[] TargetTimes { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }
        public string From { get; set; }
        public string[] To { get; set; }
        public string Cc { get; set; }
        public string[] Subject { get; set; }
        public string[] MessageBodyDay { get; set; }
        public string[] MessageBodyNight { get; set; }
        public string[] MessageBody { get; set; }
        public string AttachmentFilePath { get; set; }
    }
}