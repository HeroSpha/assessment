using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.SharedModule.Controllers;

namespace Module.SharedModule.Extensions
{
    public static class ServiceLocatorExtensions
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers()
                .ConfigureApplicationPartManager(manager =>
                {
                    manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
                });
            return services;
        }

        public static IServiceCollection AddDatabaseContext<T>(this IServiceCollection services,
            IConfiguration configuration) where T : DbContext
        {
            var connectionString = configuration.GetConnectionString("SqlLite");
            services.AddSqlLite<T>(connectionString!);
            return services;
        }
        
        private static IServiceCollection AddSqlLite<T>(this IServiceCollection services, string connectionString) where T : DbContext
        {
            services.AddDbContext<T>(m =>
                m.UseSqlite(connectionString, e => e.MigrationsAssembly(typeof(T).Assembly.FullName)));
            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<T>();
            dbContext.Database.Migrate();
            return services;
        }
    }
}
