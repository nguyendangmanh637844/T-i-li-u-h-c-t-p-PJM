using BackgroundJobExample.Models;

namespace BackgroundJobExample.ServiceInterface
{
    public interface IEmailService
    {
        Task<bool> SendMail(MineKitMailMessage message);

        MineKitMailMessage CreateMessage(EmailTemplateRequest request);
    }
}