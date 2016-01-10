using System;
using System.Threading.Tasks;
using Zuehlke.AppMonitor.Server.DataAccess.Entities;

namespace Zuehlke.AppMonitor.Server.DataAccess
{
    public interface IRepository<TEntity, in TId> where TEntity : IEntity<TId>
    {
        Task<PageResult<TEntity>> GetListAsync(PagingQuery<TEntity> query);

        Task<TEntity> GetAsync(TId id);

        Task<TEntity> CreateAsync(TEntity item);

        Task UpdateAsync(TId id, Action<TEntity> updateItem);

        Task DeleteAsync(TId id);
    }
}