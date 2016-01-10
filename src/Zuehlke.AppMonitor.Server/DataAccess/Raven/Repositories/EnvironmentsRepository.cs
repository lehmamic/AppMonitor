using System;
using System.Linq;
using System.Threading.Tasks;
using Raven.Client;
using Zuehlke.AppMonitor.Server.DataAccess.Entities;
using Environment = Zuehlke.AppMonitor.Server.DataAccess.Entities.Environment;

namespace Zuehlke.AppMonitor.Server.DataAccess.Raven.Repositories
{
    public class EnvironmentsRepository : IRepository<Environment, Guid>
    {
        private readonly IDocumentStore documentStore;
        private readonly Project project;

        public EnvironmentsRepository(IDocumentStore documentStore, Project project)
        {
            this.documentStore = documentStore;
            this.project = project;
        }

        public async Task<PageResult<Environment>> GetListAsync(PagingQuery<Environment> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            using (var session = this.documentStore.OpenAsyncSession())
            {
                RavenQueryStatistics stats;

                var environments = await session.Query<Environment>()
                    .Statistics(out stats)
                    .Where(p => p.ProjectId == this.project.Id)
                    .OrderBy(p => p.CreatedAtUtc)
                    .Skip(query.Skip)
                    .Take(query.Top)
                    .ToListAsync();

                return new PageResult<Environment>(environments.ToArray(), stats.TotalResults);
            }
        }

        public async Task<Environment> GetAsync(Guid id)
        {
            using (IAsyncDocumentSession session = this.documentStore.OpenAsyncSession())
            {
                return await session.LoadAsync<Environment>(id);
            }
        }

        public async Task<Environment> CreateAsync(Environment item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            using (IAsyncDocumentSession session = this.documentStore.OpenAsyncSession())
            {
                item.ProjectId = this.project.Id;
                item.CreatedAtUtc = DateTime.UtcNow;

                await session.StoreAsync(item);
                await session.SaveChangesAsync();

                return item;
            }
        }

        public async Task UpdateAsync(Guid id, Action<Environment> updateItem)
        {
            using (IAsyncDocumentSession session = this.documentStore.OpenAsyncSession())
            {
                var environment = await session.LoadAsync<Environment>(id);
                if (environment == null)
                {
                    throw new EntityNotFoundException($"The entity of type {typeof(Environment)} with the id {id}");
                }

                updateItem(environment);
                environment.ModifiedAtUtc = DateTime.UtcNow;

                await session.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (IAsyncDocumentSession session = this.documentStore.OpenAsyncSession())
            {
                var environment = await session.LoadAsync<Environment>(id);
                if (environment == null)
                {
                    throw new EntityNotFoundException($"The entity of type {typeof(Environment)} with the id {id}");
                }

                session.Delete(environment);
                await session.SaveChangesAsync();
            }
        }
    }
}
