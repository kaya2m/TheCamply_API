using CamplyMarket.Application.Repositories.CustomerInterface;
using CamplyMarket.Application.Repositories.FileInterface;
using CamplyMarket.Application.Repositories.InvoiceFileInterface;
using CamplyMarket.Application.Repositories.OrderInterface;
using CamplyMarket.Application.Repositories.ProductImageFileInterface;
using CamplyMarket.Application.Repositories.ProductInterface;
using CamplyMarket.Domain.Entities.Identity;
using CamplyMarket.Persistence.Context;
using CamplyMarket.Persistence.Repositories.CostumerRepositories;
using CamplyMarket.Persistence.Repositories.FileRepositories;
using CamplyMarket.Persistence.Repositories.InvoiceRepositories;
using CamplyMarket.Persistence.Repositories.OrderRepositories;
using CamplyMarket.Persistence.Repositories.ProductImageRepositories;
using CamplyMarket.Persistence.Repositories.ProductRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using CamplyMarket.Application.Abstraction.Services;
using CamplyMarket.Persistence.Services;
using CamplyMarket.Application.Abstraction.Services.Authentication;

namespace CamplyMarket.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceService(this IServiceCollection services)
        {

            ConfigurationManager configuration = new();
            configuration.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/CamplyMarket.Presentation"));
            configuration.AddJsonFile("appsettings.json");

            services.AddIdentity<AppUser,AppRole>().AddEntityFrameworkStores<CamplyDbContext>();

            services.AddDbContext<CamplyDbContext>(options =>
                 options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));

            services.AddScoped<ICostumerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICostumerWriteRepository, CostumerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRespository>();
            services.AddScoped<IInvoceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoceFileWriteRepository, InvoiceWriteRepository>();
            services.AddScoped<IProductImageFileReadRepository,ProductImagesReadRepsoitory>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImagesWriteRepsoitory>();

            services.AddScoped<IUser, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IExternalAuthentication, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();
        }
    }
}
