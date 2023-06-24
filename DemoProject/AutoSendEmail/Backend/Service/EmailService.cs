using Serilog;
using System;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using EmailServiceAPI.ServiceInterface;
using EmailServiceAPI.Models;
using CommonBase.Models;
using System.Threading.Tasks;

namespace EmailServiceAPI.Service
{
    public class EmailService : IEmailService
    {
        private readonly AppSetting _appSetting;

        public EmailService(AppSetting appSetting)
        {
            _appSetting = appSetting;
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
