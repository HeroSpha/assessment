using System.Reflection;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.GameModule.Abstractions;
using Module.GameModule.BettingFactory;
using Module.GameModule.Contracts;
using Module.GameModule.Persistence;
using Module.SharedModule.Behaviors;
using Module.SharedModule.Extensions;

namespace Module.GameModule;

public static class DependencyInjection
{
   public static IServiceCollection AddGameModule(this IServiceCollection services, IConfiguration configuration)
   {
      var config = TypeAdapterConfig.GlobalSettings;
      config.Scan(Assembly.GetExecutingAssembly());
      services.AddSingleton(config);
      services.AddScoped<IMapper, ServiceMapper>();
      services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
      services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
      services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
      services.AddScoped<IBetTypeFactory, BetTypeFactory>();
      services
         .AddDatabaseContext<RouletteWheelDbContext>(configuration);

      return services;
   }
}
