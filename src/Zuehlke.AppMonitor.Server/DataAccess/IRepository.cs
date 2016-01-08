using System;
using System.Threading.Tasks;
using Zuehlke.AppMonitor.Server.DataAccess.Entities;

namespace Zuehlke.AppMonitor.Server.DataAccess
{
    public interface IRepository<TEntity, in TId> where TEntity : IEntity<TId>
    {
        Task<PagingResult<TEntity>> GetList(PagingQuery query);

        Task<TEntity> Get(TId id);

        Task<TEntity> Create(TEntity project);

        Task Update(TId id, Action<TEntity> project);

        Task Delete(TId id);
    }
}