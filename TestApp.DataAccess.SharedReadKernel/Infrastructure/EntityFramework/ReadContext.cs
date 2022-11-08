using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace IoCore.SharedReadKernel.Infrastructure.EntityFramework
{
    internal class ReadContext : DbContext, IReadModelAccess
    {
        public DbSet<RequestForChange.RequestForChange> RequestsForChange { get; set; }

        public ReadContext(DbContextOptions<ReadContext> options) : base(options)
        {
        }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public IQueryable<T> Get<T>() where T : class
        {
            return Set<T>();
        }
    }
}