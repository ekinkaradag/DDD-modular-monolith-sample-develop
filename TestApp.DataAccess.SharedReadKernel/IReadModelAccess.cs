using System.Linq;

namespace IoCore.SharedReadKernel
{
    public interface IReadModelAccess
    {
        IQueryable<T> Get<T>() where T : class;
    }
}