using System.Reflection;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.PayoutModule.Core.Services;
using Module.PayoutModule.Payout.Contracts;
using Module.PayoutModule.Payout.PayoutMethods;
using Module.PayoutModule.Persistence;
using Module.SharedModule.Behaviors;
using Module.SharedModule.Extensions;

namespace Module.PayoutModule;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(new PayoutStrategy(
            new IPayoutService[]
            {
                new FnbWalletPayout(),
                new StandardBankInstantPayout()
            }));
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services
            .AddDatabaseContext<AccountDbContext>(configuration);
        return services;
    }
}
