using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Raven.Client;

namespace Zuehlke.AppMonitor.Server.DataAccess.Raven
{
    public class RavenDbAppMonitorDataAccess : IAppMonitorDataAccess
    {
        private readonly IDocumentStore documentStore;

        public RavenDbAppMonitorDataAccess(IDocumentStore documentStore)
        {
            if (documentStore == null)
            {
                throw new ArgumentNullException(nameof(documentStore));
            }

            this.documentStore = documentStore;
        }

        #region Implementation of IAppMonitorDataAccess
        public IProjectsRepository Projects { get; } = null;
        #endregion
    }
}
