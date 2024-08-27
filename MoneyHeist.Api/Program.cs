using FluentValidation;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace MoneyHeist.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

            builder.Host.UseLamar((context, registry) =>
            {
                registry.AddAutoMapper(typeof(Program));
                registry.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
                registry.AddValidatorsFromAssemblyContaining<Program>();

                registry.AddHttpContextAccessor();
                registry.AddControllers();

                registry.AddLocalization(options => options.ResourcesPath = "Resources");

                registry.Configure<RequestLocalizationOptions>(options =>
                {
                    var defaultCulture = new CultureInfo("en-EN");
                    CultureInfo[] supportedCultures = [defaultCulture];

                    options.DefaultRequestCulture = new RequestCulture(defaultCulture);
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
            }

            app.MapControllers();

            app.Run();
        }
    }
}