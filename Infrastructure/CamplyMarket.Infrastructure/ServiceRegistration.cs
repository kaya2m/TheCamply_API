using CamplyMarket.Application.Services;
using CamplyMarket.Infrastructure.Services;
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
            services.AddScoped<IFileService, FileService>();
        }
    }
}
