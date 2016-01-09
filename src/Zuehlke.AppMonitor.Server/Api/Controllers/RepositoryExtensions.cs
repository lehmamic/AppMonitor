using System.Threading.Tasks;
using Zuehlke.AppMonitor.Server.Api.Models;
using Zuehlke.AppMonitor.Server.DataAccess;
using Zuehlke.AppMonitor.Server.DataAccess.Entities;
using Zuehlke.AppMonitor.Server.Utils.Projection;

namespace Zuehlke.AppMonitor.Server.Api.Controllers
{
    public static class RepositoryExtensions
    {
        public static async Task<PageResultDto<TDto>> GetListAsync<TEntity, TId, TDto>(this IRepository<TEntity, TId> repository, PageQueryDto<TDto> query) where TEntity : IEntity<TId>
        {
            var pagingQuery = query.ValueOrDefault().ProjectedAs<PagingQuery<TEntity>>();

            return (await repository.GetListAsync(pagingQuery))
                    .ProjectedAs<PageResultDto<TDto>>();
        }

        public static async Task<TDto> Create<TEntity, TId, TDto>(this IRepository<TEntity, TId> repository, TDto dto)
            where TEntity : class, IEntity<TId>, new()
            where TDto : class, new()
        {
            var entity = await repository.Create(dto.ProjectedAs<TEntity>());

            return entity.ProjectedAs<TDto>();
        }

        public static async Task Update<TEntity, TId, TDto>(this IRepository<TEntity, TId> repository, TId id, TDto dto)
            where TEntity : class, IEntity<TId>, new()
            where TDto : class, new()
        {
            await repository.Update(id, p => dto.ProjectedTo(p));
        }
    }
}
