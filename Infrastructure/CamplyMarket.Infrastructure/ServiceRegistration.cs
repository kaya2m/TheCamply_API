using CamplyMarket.Application.Abstraction.Storage;
using CamplyMarket.Application.Abstraction.Token;
using CamplyMarket.Application.DTOs;
using CamplyMarket.Infrastructure.Services;
using CamplyMarket.Infrastructure.Services.Storage;
using CamplyMarket.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
            services.AddScoped<ITokenHandler,Infrastructure.Services.Token.TokenHandler>();

            services.AddScoped<IStorageService, StorageService>();
        }
        public static void AddStorage<T>(this IServiceCollection services) where T :Storage, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}
