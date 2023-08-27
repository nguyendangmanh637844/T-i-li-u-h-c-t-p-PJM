using BackgroundJobExample.CustomerException;
using BackgroundJobExample.Models;
using BackgroundJobExample.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BackgroundJobExample
{
    public class SendMail : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SendMail> _logger;
        private readonly List<TimeSpan> _targetTimes;
        private readonly EmailService _emailService;
        private readonly AppSetting _config;
        private List<Timer> _timers = new List<Timer>();

        public SendMail(IConfiguration configuration, ILogger<SendMail> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _targetTimes = LoadTargetTimes();
            _emailService = new EmailService(configuration);
            _config = configuration.GetSection("AppSettings").Get<AppSetting>() ?? new AppSetting();
        }

        private List<TimeSpan> LoadTargetTimes()
        {
            var appSettings = _configuration.GetSection("AppSettings");
            var targetTimes = appSettings.GetSection("TargetTimes").Get<List<string>>();

            var result = new List<TimeSpan>();
            foreach (var timeString in targetTimes)
            {
                if (TimeSpan.TryParse(timeString, out var time))
                {
                    result.Add(time);
                }
            }

            return result;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            foreach (var targetTime in _targetTimes)
            {
                var currentTime = DateTime.Now.TimeOfDay;
                var initialDelay = CalculateInitialDelay(currentTime, targetTime);

                if (initialDelay < TimeSpan.Zero)
                {
                    initialDelay += TimeSpan.FromDays(1);
                }

                await Task.Delay(initialDelay, stoppingToken);

                var nextExecutionTime = DateTime.Today.Add(targetTime);
                if (DateTime.Now > nextExecutionTime)
                {
                    nextExecutionTime = nextExecutionTime.AddDays(1);
                }

                var repeatInterval = nextExecutionTime - DateTime.Now;

                var timer = new Timer(AutoSendMail, null, TimeSpan.Zero, repeatInterval);
                _timers.Add(timer);
            }

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        private TimeSpan CalculateInitialDelay(TimeSpan currentTime, TimeSpan targetTime)
        {
            TimeSpan initialDelay;

            if (currentTime < targetTime)
            {
                initialDelay = targetTime - currentTime;
            }
            else
            {
                initialDelay = TimeSpan.FromDays(1) - (currentTime - targetTime);
            }

            return initialDelay;
        }

        private void AutoSendMail(object state)
        {
            EmailTemplateRequest request = new EmailTemplateRequest();
            if (String.IsNullOrEmpty(_config.From))
            {
                _logger.LogError("Chưa config from");
                return;
            }
            request.From = _config.From;
            request.Cc = _config.Cc;
            request.AttachmentFilePath = _config.AttachmentFilePath;

            string[] subject = _config.Subject;
            string[] EmailTo = _config.To;
            string[] messageDay = _config.MessageBodyDay;
            string[] messageNight = _config.MessageBodyNight;
            string[] messageRan = _config.MessageBody;
            if (EmailTo.Length == 0 || subject.Length == 0 || messageDay.Length == 0 || messageNight.Length == 0 || messageRan.Length == 0)
            {
                _logger.LogError("Chưa config đầy đủ các message");
                return;
            }

            var fromAddressNotNull = Guard.StringNotNullOrEmpty(request.From, nameof(request.From));
            if (!fromAddressNotNull)
            {
                _logger.LogError("Chưa set from");
                return;
            }
            EmailTo.ToList().ForEach(async to =>
            {
                request.To = to;
                int iS = new Random().Next(0, subject.Length);
                request.Subject = subject[iS];
                int iM = new Random().Next(0, messageDay.Length);
                int iMN = new Random().Next(0, messageNight.Length);
                int iMR = new Random().Next(0, messageRan.Length);
                DateTime currentTime = DateTime.Now;
                String body = "";
                if (IsCurrentTimeDaytime(currentTime, 1, 8))
                {
                    body = messageDay[iM];
                }
                else if (IsCurrentTimeDaytime(currentTime, 22, 23))
                {
                    body = messageNight[iMN];
                }
                else
                {
                    body = messageRan[iMR];
                }
                request.MessageBody = $"Dear em,<br/>{body}<br/>Yêu em <3";
                var message = _emailService.CreateMessage(request);
                var response = await _emailService.SendMail(message);
            });
            _logger.LogInformation("Success");
            return;
        }

        private bool IsCurrentTimeDaytime(DateTime currentTime, int start, int end)
        {
            TimeSpan startTime = new TimeSpan(start, 0, 0);
            TimeSpan endTime = new TimeSpan(end, 59, 0);

            TimeSpan currentTimeOfDay = currentTime.TimeOfDay;

            return currentTimeOfDay >= startTime && currentTimeOfDay <= endTime;
        }

        public override void Dispose()
        {
            foreach (var timer in _timers)
            {
                timer?.Dispose();
            }
            base.Dispose();
        }
    }
}