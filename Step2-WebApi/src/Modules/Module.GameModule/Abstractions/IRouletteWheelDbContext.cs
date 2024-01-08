using Microsoft.EntityFrameworkCore;
using Module.GameModule.Entities;

namespace Module.GameModule.Abstractions;

public interface IRouletteWheelDbContext 
{
    Task<int> SaveChangesAsync();
    public DbSet<RouletteWheelSession> RouletteWheelSessions { get; set; }
}