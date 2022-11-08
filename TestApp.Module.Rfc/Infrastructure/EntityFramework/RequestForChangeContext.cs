using Microsoft.EntityFrameworkCore;
using TestApp.Module.Rfc.Domain.RequestForChange;

namespace TestApp.Module.Rfc.Infrastructure.EntityFramework
{
    internal class RequestForChangeContext : DbContext
    {
        public DbSet<RequestForChange> RequestsForChange { get; set; } = null!;

        public RequestForChangeContext(DbContextOptions<RequestForChangeContext> options) : base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("rfc");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}