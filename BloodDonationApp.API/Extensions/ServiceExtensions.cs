using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.BusinessLogic.Services.Implementation;
using BloodDonationApp.DataAccessLayer.UnitOfWork;
using BloodDonationApp.Infrastructure;
using BloodDonationApp.LoggerService;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;
using BloodDonationApp.Domain.DomainModel;
using Microsoft.AspNetCore.Identity;

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

        public static void AddCustomMediaTypes(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(config =>
            {
                var systemTextJsonOutputFormatter = config.OutputFormatters
                .OfType<SystemTextJsonOutputFormatter>()?
                .FirstOrDefault();
                if (systemTextJsonOutputFormatter != null)
                {
                    systemTextJsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.dunja.hateoas+json");
                    Console.WriteLine("Registered custom media type: application/vnd.dunja.hateoas+json");
                }
                var xmlOutputFormatter = config.OutputFormatters
                .OfType<XmlDataContractSerializerOutputFormatter>()?
                .FirstOrDefault();
                if (xmlOutputFormatter != null)
                {
                    xmlOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.dunja.hateoas+xml");
                    Console.WriteLine("Registered custom media type: application/vnd.dunja.hateoas+xml");
                }
            });
        }
        public static void ConfigureResponseCaching(this IServiceCollection services) => services.AddResponseCaching();
        public static void ConfigureOutputCaching(this IServiceCollection services) => services.AddOutputCache(opt =>
        {
            opt.AddBasePolicy(bp => bp.Expire(TimeSpan.FromSeconds(42)));
            opt.AddPolicy("120SecondsDuration", p => p.Expire(TimeSpan.FromSeconds(120)));
        });

        public static void ConfigureRateLimitingOptions(this IServiceCollection services)
        {
            services.AddRateLimiter(opt =>
            {
                opt.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                RateLimitPartition.GetFixedWindowLimiter("GlobalLimiter",
                partition => new FixedWindowRateLimiterOptions
                {
                    AutoReplenishment = true,
                    PermitLimit = 13,
                    QueueLimit = 1,
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    Window = TimeSpan.FromMinutes(1)
                }));

                opt.AddPolicy("SpecificActionsPolicy", context => 
                RateLimitPartition.GetFixedWindowLimiter("SpecificLimiter",
                partition => new FixedWindowRateLimiterOptions
                {
                    AutoReplenishment = true,
                    PermitLimit = 3,
                    Window = TimeSpan.FromSeconds(13)
                }));

                //opt.RejectionStatusCode = 429;
                opt.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = 429;
                    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                        await context.HttpContext.Response
                        .WriteAsync($"Too many requests. Please try again after { retryAfter.TotalSeconds} second(s).", token);
                    else
                        await context.HttpContext.Response
                        .WriteAsync("Too many requests. Please try again later.", token);
                };
            });
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<BloodDonationContext>()
            .AddDefaultTokenProviders();
        }

    }
}
