using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.BusinessLogic.Services.Implementation;
using BloodDonationApp.DataAccessLayer.UnitOfWork;
using BloodDonationApp.DataTransferObject.Donors; //proveri dal sme
using BloodDonationApp.DataTransferObject.Mappers; //proveri dal sme
using BloodDonationApp.Domain.DomainModel; //proveri dal sme
using BloodDonationApp.Infrastructure;
using BloodDonationApp.LoggerService;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationApp.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
        }
        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, ConsoleLogger>();
        public static void ConfigureSqlServerContext(this IServiceCollection services, IConfiguration configuration) => services.AddDbContext<BloodDonationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        public static void ConfigureUnitOfWork(this IServiceCollection services) =>
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();


    }
}
