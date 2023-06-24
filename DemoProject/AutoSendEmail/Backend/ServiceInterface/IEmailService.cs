using CommonBase.Models;
using EmailServiceAPI.Models;
using System.Threading.Tasks;

namespace EmailServiceAPI.ServiceInterface
{
    public interface IEmailService
    {
        Task<bool> SendMail(MineKitMailMessage message);
        MineKitMailMessage CreateMessage(EmailTemplateRequest request);
    }
}
