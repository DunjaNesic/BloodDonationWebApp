using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.BusinessLogic.Services.Implementation;
using BloodDonationApp.DataAccessLayer.UnitOfWork;
using BloodDonationApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.Extensions.Options;
using NLog;
using BloodDonationApp.LoggerService;
using BloodDonationApp.API.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using BloodDonationApp.Presentation.ActionFilters;
using BloodDonationApp.DataTransferObject.Action;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using BloodDonationApp.Presentation;

var builder = WebApplication.CreateBuilder(args);

//LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.AddScoped<ValidationFilterAttribute>();

// Add services to the container.
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<BloodDonationContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IVolunteerService, VolunteerService>();
builder.Services.AddScoped<IDataShaper<GetTransfusionActionDTO>, DataShaper<GetTransfusionActionDTO>>();
//builder.Services.AddScoped<ValidateMediaTypeAttribute>();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureCors();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureSqlServerContext(builder.Configuration);
builder.Services.ConfigureUnitOfWork();
builder.Services.ConfigureServiceManager();
//builder.Services.ConfigureResponseCaching();
//builder.Services.ConfigureOutputCaching();
builder.Services.ConfigureRateLimitingOptions();
builder.Services.AddAuthentication();
builder.Services.ConfigureJWT(builder.Configuration);


builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = false;

    //necemo response cachinggg
    //config.CacheProfiles.Add("120SecondsDuration", new CacheProfile
    //{
    //    Duration = 120
    //});

})
    .AddNewtonsoftJson()   
    //.AddXmlDataContractSerializerFormatters()
    .AddApplicationPart(typeof(BloodDonationApp.Presentation.AssemblyReference).Assembly);

builder.Services.AddCustomMediaTypes();

builder.Services.AddScoped<ValidateMediaTypeAttribute>();

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

var app = builder.Build();

//var logger = app.Services.GetRequiredService<ILoggerManager>();
//app.ConfigureExceptionHandler(logger);

if (app.Environment.IsProduction())
    app.UseHsts();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseRateLimiter();
app.UseCors("CorsPolicy");
//app.UseResponseCaching();
//app.UseOutputCache();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();