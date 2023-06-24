using CommonBase.CustomerException;
using CommonBase.Models;
using EmailServiceAPI.Models;
using EmailServiceAPI.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using SendEmail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly AppSetting _config;

        public EmailController(IEmailService emailService, AppSetting config)
        {
            _emailService = emailService;
            _config = config;
        }

        [HttpGet]
        [Route("send-email")]
        public async Task<string> ReceiveEmailRequest()
        {
            EmailTemplateRequest request = new EmailTemplateRequest();
            if(String.IsNullOrEmpty(_config.From) || String.IsNullOrEmpty(_config.Day) || String.IsNullOrEmpty(_config.Night)) return null;
            request.From = _config.From;
            request.Cc = _config.Cc;
            request.AttachmentFilePath = _config.AttachmentFilePath;

            string[] subject = _config.Subject;
            string[] EmailTo = _config.To;
            string[] messageDay = _config.MessageBodyDay;
            string[] messageNight = _config.MessageBodyNight;
            string[] messageRan = _config.MessageBody;
            if (EmailTo.Length == 0 || subject.Length == 0 || messageDay.Length == 0 || messageNight.Length == 0 || messageRan.Length == 0) return null;

            var fromAddressNotNull = Guard.StringNotNullOrEmpty(request.From, nameof(request.From));
            if (!fromAddressNotNull)
            {
                return null;
            }
            EmailTo.ToList().ForEach(async to =>
            {
                request.To = to;
                int iS = new Random().Next(0, subject.Length);
                request.Subject = subject[iS];
                int iM = new Random().Next(0, messageDay.Length);
                int iMN = new Random().Next(0, messageNight.Length);
                int iMR = new Random().Next(0, messageRan.Length);
                String time = DateTime.Now.ToString("HH:mm");

                if (time == _config.Day)
                {
                    request.MessageBody = messageDay[iM];
                }
                else if (time == _config.Night)
                {
                    request.MessageBody = messageNight[iMN];
                }
                else
                {
                    request.MessageBody = messageRan[iMR];
                }
                var message = _emailService.CreateMessage(request);
                var response = await _emailService.SendMail(message);
            });
            return "OK";
        }

        [HttpGet]
        [Route("check")]
        public object check()
        {
            return new { name="àdas", age="12"};
        }
        [HttpGet]
        [Route("get-id")]
        public string checkID(string id)
        {
            return id;
        }
        [HttpGet]
        [Route("get-by-id/{id}")]
        public string checkbyID(string id)
        {
            return id;
        }
        [HttpPut]
        [Route("put")]
        public DayNight Put(DayNight id)
        {
            return id;
        }
        [HttpPost]
        [Route("post")]
        public DayNight post(DayNight id)
        {
            return id;
        }
    }
}