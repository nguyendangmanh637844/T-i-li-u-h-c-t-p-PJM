using BackgroundJobExample.Models;
using BackgroundJobExample.ServiceInterface;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Serilog;

namespace BackgroundJobExample.Service
{
    public class EmailService : IEmailService
    {
        private readonly AppSetting _appSetting;

        public EmailService(IConfiguration configuration)
        {
            _appSetting = configuration.GetSection("AppSettings").Get<AppSetting>() ?? new AppSetting();
        }

        public async Task<bool> SendMail(MineKitMailMessage message)
        {
            bool isSendEmailSucess = false;

            try
            {
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_appSetting.SmtpHost, _appSetting.SmtpPort, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_appSetting.SmtpUser, _appSetting.SmtpPass);
                await smtp.SendAsync(message);

                isSendEmailSucess = true;
            }
            catch (Exception ex)
            {
                Log.Error($"Can not send email to {message.To}: {ex.Message}");
            }

            return isSendEmailSucess;
        }

        private MimeEntity GetBodyBuilder(string templateBody, string attachmentFile)
        {
            var builder = new BodyBuilder();
            builder.HtmlBody = templateBody;
            if (!string.IsNullOrEmpty(attachmentFile) && System.IO.File.Exists(attachmentFile))
            {
                builder.Attachments.Add(attachmentFile);
            }
            return builder.ToMessageBody();
        }

        public MineKitMailMessage CreateMessage(EmailTemplateRequest request)
        {
            var message = new MineKitMailMessage();
            message.From.Add(new MailboxAddress(request.From, request.From));
            message.To.Add(MailboxAddress.Parse(request.To));
            if (!string.IsNullOrEmpty(request.Cc))
            {
                message.Cc.Add(MailboxAddress.Parse(request.Cc));
            }
            message.Subject = request.Subject;
            message.Body = GetBodyBuilder(request.MessageBody, request.AttachmentFilePath);

            return message;
        }
    }
}