using System;
using System.Threading.Tasks;

namespace Zuehlke.AppMonitor.Server.DataAccess
{
    public interface IRepositoryCollection<TEntity, TId> where TEntity : IEntity<TId>
    {
        Task<IRepository<TEntity, TId>> GetAsync(Guid key);
    }
}