using System;
using System.Threading.Tasks;
using Raven.Client;
using Zuehlke.AppMonitor.Server.DataAccess.Entities;

namespace Zuehlke.AppMonitor.Server.DataAccess.Raven
{
    public class RepositoryCollection<TEntity, TId> : IRepositoryCollection<TEntity, TId> where TEntity : IEntity<TId>
    {
        private readonly IDocumentStore documentStore;
        private readonly Func<IDocumentStore, Project, IRepository<TEntity, TId>> factory;

        public RepositoryCollection(IDocumentStore documentStore, Func<IDocumentStore, Project, IRepository<TEntity, TId>> factory)
        {
            if (documentStore == null)
            {
                throw new ArgumentNullException(nameof(documentStore));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            this.documentStore = documentStore;
            this.factory = factory;
        }

        #region Implementation of IRepositoryCollection<TEntity,in TId>
        public async Task<IRepository<TEntity, TId>> GetAsync(Guid key)
        {
            using (IAsyncDocumentSession session = this.documentStore.OpenAsyncSession())
            {
                var project = await session.LoadAsync<Project>(key);
                if (project == null)
                {
                    return null;
                }
                else
                {
                    return this.factory(this.documentStore, project);
                }

            }
        }
        #endregion
    }
}