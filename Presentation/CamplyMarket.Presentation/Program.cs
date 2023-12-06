using CamplyMarket.Application;
using CamplyMarket.Application.Validators.Products;
using CamplyMarket.Infrastructure;
using CamplyMarket.Infrastructure.Filters;
using CamplyMarket.Infrastructure.Services.Storage.Azure;
using CamplyMarket.Infrastructure.Services.Storage.Local;
using CamplyMarket.Persistence;
using CamplyMarket.Persistence.Context;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CamplyMarket.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddPersistenceService();
            builder.Services.AddInfrastrustureServices();
            builder.Services.AddApplicationServices();
            builder.Services.AddStorage<AzureStorage>();

            builder.Services.AddControllers(options => options.Filters.Add<ValidationFilters>())
                .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<CreateProductValidatior>())
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(opt => opt.AddDefaultPolicy(poilcy => poilcy
            .WithOrigins("http://localhost:4200", "https://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors();

            app.MapControllers();

            app.Run();
        }
    }
}