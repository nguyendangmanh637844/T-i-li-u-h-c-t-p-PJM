using Microsoft.Extensions.Configuration;

internal class Program
{
    private static System.Threading.Timer timer; // Đảm bảo timer là biến toàn cục

    private static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var appSettings = configuration.GetSection("AppSettings");
        var repeatIntervalSeconds = appSettings["RepeatIntervalSeconds"];

        if (int.TryParse(repeatIntervalSeconds, out int intervalSeconds))
        {
            timer = new System.Threading.Timer(TaskToDo, null, TimeSpan.Zero, TimeSpan.FromSeconds(intervalSeconds));
        }
        else
        {
            Console.WriteLine("Invalid RepeatIntervalSeconds value.");
            return;
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();

        // Đảm bảo timer đã được hủy trước khi thoát khỏi chương trình
        timer.Dispose();
    }

    private static void TaskToDo(object state)
    {
        Console.WriteLine("Hello");
    }
}