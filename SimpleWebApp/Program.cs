using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SimpleWebApp.Infrastructure;

namespace SimpleWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            BuildAppSettingsProvider(builder.Configuration);

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<SimpleWebAppContext>(options =>
                    options.UseNpgsql(AppSettingsProvider.Configuration.GetConnectionString("SimpleWebDb")), ServiceLifetime.Scoped);

            //registred DI container
            builder.Services.AddSimpleWebAppRepository();

            builder.Services.AddCors(GetCorsOptions());
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            {
                using var scope = app.Services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<SimpleWebAppContext>();
                db.Database.EnsureCreated();

                app.UseExceptionHandler("/Error");
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(AppSettingsProvider.DefaultCorsPolicyName);

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.Run();
        }

        private static void BuildAppSettingsProvider(IConfiguration configuration)
        {
            AppSettingsProvider.DbConnectionString = configuration.GetConnectionString("SimpleWebDb");
            AppSettingsProvider.DefaultCorsPolicyName = configuration.GetSection("AllowOrigins").Value;
            AppSettingsProvider.Configuration = configuration;
        }

        private static Action<CorsOptions> GetCorsOptions()
        {
            return options =>
            {
                options.AddPolicy(AppSettingsProvider.DefaultCorsPolicyName, opt =>
                {
                    var allowed = AppSettingsProvider.Configuration.GetSection(AppSettingsProvider.DefaultCorsPolicyName)
                        .GetChildren()
                        .Select(x => x.Value)
                        .ToArray();

                    opt.WithOrigins(allowed)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            };
        }
    }

    public static class AppSettingsProvider
    {
        public static string DbConnectionString { get; set; }
        public static string DefaultCorsPolicyName { get; set; }
        public static IConfiguration Configuration { get; set; }
    }
}