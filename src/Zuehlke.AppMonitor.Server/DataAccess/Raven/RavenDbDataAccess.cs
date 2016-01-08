using System;
using Raven.Client;
using Zuehlke.AppMonitor.Server.DataAccess.Entities;
using Zuehlke.AppMonitor.Server.DataAccess.Raven.Repositories;

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
        }

        #region Implementation of IDataAccess
        public IRepository<Project, string> Projects { get; }
        #endregion
    }
}
