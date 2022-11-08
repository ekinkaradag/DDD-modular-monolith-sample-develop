using System;
using System.Threading.Tasks;

namespace TestApp.BuildingBlocks.Domain
{
    public interface IRepository<TAggregate> where TAggregate : IAggregateRoot
    {
        Task AddAsync(TAggregate aggregate);

        Task<TAggregate> GetByIdAsync(Guid id);
    }
}