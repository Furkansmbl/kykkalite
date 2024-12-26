using Humanizer;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Kykkalite_UI
{
    public class StartupCs
    {
        public StartupCs(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
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
                // Generic error handler for production
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();

                // Use status code pages to show custom messages for HTTP error codes
                app.UseStatusCodePages(context =>
                {
                    context.HttpContext.Response.ContentType = "text/plain";
                    return context.HttpContext.Response.WriteAsync(
                        $"An error occurred. Status code: {context.HttpContext.Response.StatusCode}");
                });
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                // Default route for non-area controllers
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
