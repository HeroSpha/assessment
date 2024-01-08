using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Module.PayoutModule.Entities;
using Module.SharedModule.Persistence;

namespace Module.PayoutModule.Persistence
{
    public class AccountDbContext(DbContextOptions options) : ModuleDbContext(options)
    {
        protected override string Schema  => "Payout";

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

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Entities.Payout> Payouts { get; set; }
    }
}
