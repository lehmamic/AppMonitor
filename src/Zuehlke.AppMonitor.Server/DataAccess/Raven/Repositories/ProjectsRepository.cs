using System;
using System.Linq;
using System.Threading.Tasks;
using Raven.Client;
using Zuehlke.AppMonitor.Server.DataAccess.Entities;

namespace Zuehlke.AppMonitor.Server.DataAccess.Raven.Repositories
{
    public class ProjectsRepository :IRepository<Project, string>
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
        public async Task<PageResult<Project>> GetList(PagingQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            using (var session = this.documentStore.OpenAsyncSession())
            {
                RavenQueryStatistics stats;

                var projects = await session.Query<Project>()
                    .Statistics(out stats)
                    .Skip(query.Skip)
                    .Take(query.Top)
                    .ToListAsync();

                return new PageResult<Project>(projects.ToArray(), stats.TotalResults);
            }
        }

        public async Task<Project> Get(string id)
        {
            using (IAsyncDocumentSession session = this.documentStore.OpenAsyncSession())
            {
                return await session.LoadAsync<Project>(id);
            }
        }

        public async Task<Project> Create(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            using (IAsyncDocumentSession session = this.documentStore.OpenAsyncSession())
            {
                project.CreatedAtUtc = DateTime.UtcNow;

                await session.StoreAsync(project);
                await session.SaveChangesAsync();

                return project;
            }
        }

        public async Task Update(string id, Action<Project> updateItem)
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

        public async Task Delete(string id)
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