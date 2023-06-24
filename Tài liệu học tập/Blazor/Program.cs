using Blazored.LocalStorage;
using Blazored.Toast;
using DemoBlazor.Service;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace DemoBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddBlazoredToast();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddSingleton<DialogService>();
            builder.Services.AddScoped<IClassService, ClassService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            await builder.Build().RunAsync();
        }
    }
}