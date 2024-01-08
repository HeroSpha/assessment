using Microsoft.Extensions.DependencyInjection;

namespace Module.HistoryModule;

public static class HistoryModuleDependencyInjection
{
    public static IServiceCollection AddHistoryModule(this IServiceCollection services)
    {
        return services;
    }
}
