using System;
using System.Linq;
using System.Threading.Tasks;
using Raven.Client;
using Zuehlke.AppMonitor.Server.DataAccess.Entities;
using Zuehlke.AppMonitor.Server.DataAccess.Raven.Indeces;

namespace Zuehlke.AppMonitor.Server.DataAccess.Raven.Repositories
{
    public class ProjectsRepository :IRepository<Project, Guid>
    {
        private readonly IDocumentStore documentStore;

        public ProjectsRepository(IDocumentStore documentStore)
        {
            if (documentStore == null)
            {
                throw new ArgumentNullException(nameof(documentStore));
            }

            this.documentStore = documentStore;
        }

        #region Implementation of IRepository<Project>
        public async Task<PageResult<Project>> GetListAsync(PagingQuery<Project> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            using (var session = this.documentStore.OpenAsyncSession())
            {
                RavenQueryStatistics stats;

                var projects = await session.Query<Project, Projects_ByCreatedAtUtc>()
                    .Statistics(out stats)
                    .OrderBy(p => p.CreatedAtUtc)
                    .Skip(query.Skip)
                    .Take(query.Top)
                    .ToListAsync();

                return new PageResult<Project>(projects.ToArray(), stats.TotalResults);
            }
        }

        public async Task<Project> GetAsync(Guid id)
        {
            using (IAsyncDocumentSession session = this.documentStore.OpenAsyncSession())
            {
                return await session.LoadAsync<Project>(id);
            }
        }

        public async Task<Project> CreateAsync(Project item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            using (IAsyncDocumentSession session = this.documentStore.OpenAsyncSession())
            {
                item.CreatedAtUtc = DateTime.UtcNow;

                await session.StoreAsync(item);
                await session.SaveChangesAsync();

                return item;
            }
        }

        public async Task UpdateAsync(Guid id, Action<Project> updateItem)
        {
            using (IAsyncDocumentSession session = this.documentStore.OpenAsyncSession())
            {
                var project = await session.LoadAsync<Project>(id);
                if (project == null)
                {
                    throw new EntityNotFoundException($"The entity of type {typeof(Project)} with the id {id}");
                }

                updateItem(project);
                project.ModifiedAtUtc = DateTime.UtcNow;

                await session.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (IAsyncDocumentSession session = this.documentStore.OpenAsyncSession())
            {
                var project = await session.LoadAsync<Project>(id);
                if (project == null)
                {
                    throw new EntityNotFoundException($"The entity of type {typeof(Project)} with the id {id}");
                }

                session.Delete(project);
                await session.SaveChangesAsync();
            }
        }
        #endregion
    }
}