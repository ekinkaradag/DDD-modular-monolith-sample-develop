using System;
using System.Threading.Tasks;
using TestApp.BuildingBlocks.Domain;

namespace TestApp.Module.Rfc.Domain.RequestForChange
{
    internal interface IRfcRepository : IRepository<RequestForChange>
    {
        Task CommitAsync();
    }
}