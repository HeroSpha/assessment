using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Module.GameModule.Abstractions;
using Module.GameModule.Entities;
using Module.GameModule.Helpers;
using Module.SharedModule.Persistence;

namespace Module.GameModule.Persistence;

public class RouletteWheelDbContext(DbContextOptions options) : ModuleDbContext(options)
{
    protected override string Schema => RouletteWheelConstants.Schema;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
       ArgumentNullException.ThrowIfNull(modelBuilder);
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }

    public DbSet<RouletteWheelSession> RouletteWheelSessions { get; set; }
    public DbSet<SessionBet> SessionBets { get; set; }
}