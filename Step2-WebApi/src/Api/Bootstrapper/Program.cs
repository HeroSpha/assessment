using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using Module.GameModule;
using Module.HistoryModule;
using Module.PayoutModule;
using Module.SharedModule.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Derivco Roullete Game",
        Description = "Spinner Game API",
        TermsOfService = new Uri($"https://gamesurl.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Contact",
            Url = new Uri($"https://gamesurl.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri($"https://gamesurl.com/License")
        }
    });
});

builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services
    .AddSharedServices(configuration)
    .AddGameModule(configuration)
    .AddHistoryModule()
    .AddPayoutModules();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler("/error");

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

