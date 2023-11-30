using CamplyMarket.Application.Abstraction.Storage;
using CamplyMarket.Infrastructure.Services;
using CamplyMarket.Infrastructure.Services.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamplyMarket.Infrastructure
{
    public static class ServiceRegistration 
    {
        public static void AddInfrastrustureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
        }
        public static void AddStorage<T>(this IServiceCollection services) where T :Storage, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}
