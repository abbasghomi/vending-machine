using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using VendingMachine.Application;
using VendingMachine.Infrastructure;
using VendingMachine.Infrastructure.Persistence;
using VendingMachine.Presentation.Common.Middlewares;

namespace VendingMachine.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMaintenance(() => Configuration.GetValue<bool>("WebsiteVariables:MaintenanceMode"),
               Encoding.UTF8.GetBytes(""));

            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            services.AddControllersWithViews();

            services.AddRazorPages();

            services.AddMvc();

            services.AddCors(
                options => options.AddDefaultPolicy(
                    builder => builder.AllowAnyOrigin()
                    )
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors();

            app.UseMaintenance();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}",
                    defaults: new { controller = "Home", action = "Index", });
                endpoints.MapRazorPages();
            });
        }
    }
}
