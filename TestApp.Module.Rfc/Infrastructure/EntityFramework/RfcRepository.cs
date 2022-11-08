using System;
using System.Threading.Tasks;
using TestApp.Module.Rfc.Domain.RequestForChange;

namespace TestApp.Module.Rfc.Infrastructure.EntityFramework
{
    internal class RfcRepository : IRfcRepository
    {
        private readonly RequestForChangeContext _dbContext;

        public RfcRepository(RequestForChangeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(RequestForChange rfc)
        {
            await _dbContext.RequestsForChange.AddAsync(rfc);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<RequestForChange> GetByIdAsync(Guid id)
        {
            var rfc = await _dbContext.RequestsForChange.FindAsync(id);

            return rfc;
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}