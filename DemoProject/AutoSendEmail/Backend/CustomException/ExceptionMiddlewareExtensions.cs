using CommonBase.CustomerException;
using Microsoft.AspNetCore.Builder;

namespace EmailServiceAPI.CustomException
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
