using CamplyMarket.Application.Repositories.CustomerInterface;
using CamplyMarket.Application.Repositories.OrderInterface;
using CamplyMarket.Application.Repositories.ProductInterface;
using CamplyMarket.Persistence.Context;
using CamplyMarket.Persistence.Repositories.CostumerRepositories;
using CamplyMarket.Persistence.Repositories.OrderRepositories;
using CamplyMarket.Persistence.Repositories.ProductRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamplyMarket.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceService(this IServiceCollection services)
        {

            ConfigurationManager configuration = new();
            configuration.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/CamplyMarket.Presentation"));
            configuration.AddJsonFile("appsettings.json");


            services.AddDbContext<CamplyDbContext>(options =>
                 options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));

            services.AddScoped<ICostumerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICostumerWriteRepository, CostumerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        }
    }
}
