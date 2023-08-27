using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BackgroundJobExample
{
    public class HelloPrintingService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HelloPrintingService> _logger;
        private readonly List<TimeSpan> _targetTimes;
        private List<Timer> _timers = new List<Timer>();

        public HelloPrintingService(IConfiguration configuration, ILogger<HelloPrintingService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _targetTimes = LoadTargetTimes();
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

                var timer = new Timer(PrintHello, null, TimeSpan.Zero, repeatInterval);
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

        private void PrintHello(object state)
        {
            _logger.LogInformation("Hello");
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