using FluentEmail.Smtp;
using Kykkalite_UI;
using Kykkalite_UI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Net;


Log.Logger = new LoggerConfiguration()
.MinimumLevel.Warning()
.WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
.CreateLogger();

try
{

    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddSerilog();
    // Remove the default mapping for the 'sub' claim
    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

    // FluentEmail setup with Gmail SMTP
    builder.Services.AddFluentEmail("furkansumbul1903@gmail.com")
        .AddRazorRenderer()
        .AddMailKitSender(new FluentEmail.MailKitSmtp.SmtpClientOptions()
        {
            Server = "smtp.gmail.com",
            User = "furkansumbul1903@gmail.com",
            Password = "wtqj ttiv xtgy doxs",
            UseSsl = true,
            RequiresAuthentication = true,
            SocketOptions = MailKit.Security.SecureSocketOptions.StartTls,
            Port = 587,
        });
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();
    // Add HttpClient and other services
    builder.Services.AddHttpClient();
    builder.Services.AddControllersWithViews();
    builder.Services.AddHttpContextAccessor();

    // JWT & Cookie Authentication
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
        {
            opt.LoginPath = "/Login/Index/";
            opt.LogoutPath = "/Login/Index/";
            opt.AccessDeniedPath = "/Fabrikalar/Index/";
            opt.Cookie.HttpOnly = true; // Ensure HttpOnly for security
            opt.Cookie.SameSite = SameSiteMode.Strict;
            opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            opt.Cookie.Name = "ipktoken";
        });

    // Add scoped services
    builder.Services.AddScoped<ILoginService, LoginService>();

    var app = builder.Build();
   
    // Configure the HTTP request pipeline for production
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler();
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    // Authentication and Authorization (order matters)
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Login}/{action=Index}/{id?}");

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal("Uygulama Problem yaþýyor");
}
finally
{
    Log.CloseAndFlush();
}
