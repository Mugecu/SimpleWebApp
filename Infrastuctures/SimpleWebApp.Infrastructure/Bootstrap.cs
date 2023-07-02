using Microsoft.Extensions.DependencyInjection;
using SimpleWebApp.Domain.Abstracts;
using SimpleWebApp.Domain.Entities;
using SimpleWebApp.Infrastructure.Repositories;
using System.Runtime.CompilerServices;

namespace SimpleWebApp.Infrastructure
{
    public static class Bootstrap
    {
        public static IServiceCollection AddSimpleWebAppRepository(this IServiceCollection services)
        {
            services.AddScoped<Repository<Product>, ProductEfRepository>();
            services.AddScoped<Repository<Warehouse>, WarehouseEfRepository>();
            return services;
        }
    }
}