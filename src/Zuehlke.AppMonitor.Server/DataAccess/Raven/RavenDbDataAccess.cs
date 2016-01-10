using System;
using Raven.Client;
using Zuehlke.AppMonitor.Server.DataAccess.Entities;
using Zuehlke.AppMonitor.Server.DataAccess.Raven.Repositories;
using Environment = Zuehlke.AppMonitor.Server.DataAccess.Entities.Environment;

namespace Zuehlke.AppMonitor.Server.DataAccess.Raven
{
    public class RavenDbDataAccess : IDataAccess
    {
        private readonly IDocumentStore documentStore;

        public RavenDbDataAccess(IDocumentStore documentStore)
        {
            if (documentStore == null)
            {
                throw new ArgumentNullException(nameof(documentStore));
            }

            this.documentStore = documentStore;

            this.Projects = new ProjectsRepository(this.documentStore);
            this.Environments = new RepositoryCollection<Environment, Guid>(this.documentStore, (s, p) => new EnvironmentsRepository(s, p));
        }

        #region Implementation of IDataAccess
        public IRepository<Project, Guid> Projects { get; }

        public IRepositoryCollection<Environment, Guid> Environments { get; }
        #endregion
    }
}
