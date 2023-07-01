using Microsoft.AspNetCore.Cors.Infrastructure;

namespace SimpleWebApp
{
    public class Program
    {
        private const string _defaultCorsPolicyName = "AllowOrigins";
        public IConfiguration Configuration { get; }

        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            //// Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(_defaultCorsPolicyName);

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }

        private Action<CorsOptions> GetCorsOptions()
        {
            return options =>
            {
                options.AddPolicy(_defaultCorsPolicyName, opt =>
                {
                    var allowed = Configuration.GetSection(_defaultCorsPolicyName)
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
}