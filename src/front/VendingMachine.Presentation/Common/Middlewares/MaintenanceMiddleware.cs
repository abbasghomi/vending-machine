using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace VendingMachine.Presentation.Common.Middlewares
{
    public class MaintenanceMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;
        private readonly MaintenanceWindow window;

        public MaintenanceMiddleware(RequestDelegate next, MaintenanceWindow window, ILogger<MaintenanceMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
            this.window = window;
        }

        public async Task Invoke(HttpContext context)
        {
            if (window.Enabled)
            {
                // set the code to 503 for SEO reasons
                context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                context.Response.Headers.Add("Retry-After", window.RetryAfterInSeconds.ToString());
                context.Response.ContentType = window.ContentType;
                if (window.Response.Length == 0)
                {
                    context.Response.Redirect("maintenance/maintenance.html");
                }
                else
                {
                    await context
                        .Response
                        .WriteAsync(Encoding.UTF8.GetString(window.Response), Encoding.UTF8);
                }
            }
            await next.Invoke(context);
        }
    }

    public class MaintenanceWindow
    {

        private Func<bool> enabledFunc;
        private byte[] response;

        public MaintenanceWindow(Func<bool> enabledFunc, byte[] response)
        {
            this.enabledFunc = enabledFunc;
            this.response = response;
        }

        public bool Enabled => enabledFunc();
        public byte[] Response => response;

        public int RetryAfterInSeconds { get; set; } = 3600;
        public string ContentType { get; set; } = "text/html";
    }

    public static class MaintenanceWindowExtensions
    {
        public static IServiceCollection AddMaintenance(this IServiceCollection services, MaintenanceWindow window)
        {
            services.AddSingleton(window);
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="enabler"></param>
        /// <param name="response">Encoding.UTF8.GetBytes("&lt;div&gt;Under Maintenance!&lt;/div&gt;")</param>
        /// <param name="contentType"></param>
        /// <param name="retryAfterInSeconds"></param>
        /// <returns></returns>
        public static IServiceCollection AddMaintenance(this IServiceCollection services, Func<bool> enabler, byte[] response, string contentType = "text/html", int retryAfterInSeconds = 3600)
        {
            AddMaintenance(services, new MaintenanceWindow(enabler, response)
            {
                ContentType = contentType,
                RetryAfterInSeconds = retryAfterInSeconds
            });

            return services;
        }

        public static IApplicationBuilder UseMaintenance(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MaintenanceMiddleware>();
        }
    }
}
