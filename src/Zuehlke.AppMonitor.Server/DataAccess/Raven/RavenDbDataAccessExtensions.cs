using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;

#if DNX451
using Raven.Client.Embedded;
#endif

namespace Zuehlke.AppMonitor.Server.DataAccess.Raven
{
    public static class RavenDbDataAccessExtensions
    {
        public static IServiceCollection AddRavenDbDataAccess(this IServiceCollection serviceCollection, IConfigurationRoot configuration)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            serviceCollection.AddSingleton(sp => CreateDocumentStore(configuration));
            serviceCollection.AddSingleton<IAppMonitorDataAccess, RavenDbAppMonitorDataAccess>();

            return serviceCollection;
        }

        private static IDocumentStore CreateDocumentStore(IConfigurationRoot configuration)
        {
            var connectionString = configuration["Data:ConnectionString"];
            bool useEmbeddedStore = connectionString.Contains("DataDir") || connectionString.Contains("RunInMemory");

            IDocumentStore documentStore;
#if DNX451
            documentStore = useEmbeddedStore ? CreateEmbeddableDocumentStore(connectionString) : CreateDocumentStore(connectionString);
#endif

#if DNXCORE50
            documentStore = CreateDocumentStore(connectionString);
#endif
            return documentStore;
        }

        private static IDocumentStore CreateDocumentStore(string connectionString)
        {
            var parser = ConnectionStringParser<RavenConnectionStringOptions>.FromConnectionString(connectionString);
            parser.Parse();

            var options = parser.ConnectionStringOptions;
            var documentStore = new DocumentStore();

            if (options != null && options.ResourceManagerId != Guid.Empty)
            {
                documentStore.ResourceManagerId = options.ResourceManagerId;
            }

            if (options.Credentials != null)
            {
                documentStore.Credentials = options.Credentials;
            }

            if (string.IsNullOrEmpty(options.Url) == false)
            {
                documentStore.Url = options.Url;
            }

            if (string.IsNullOrEmpty(options.DefaultDatabase) == false)
            {
                documentStore.DefaultDatabase = options.DefaultDatabase;
            }

            if (string.IsNullOrEmpty(options.ApiKey) == false)
            {
                documentStore.ApiKey = options.ApiKey;
            }

            if (options.FailoverServers != null)
            {
                documentStore.FailoverServers = options.FailoverServers;
            }


#if DNX451
            documentStore.EnlistInDistributedTransactions = options.EnlistInDistributedTransactions;
#endif

            return documentStore;
        }

#if DNX451
        private static IDocumentStore CreateEmbeddableDocumentStore(string connectionString)
        {
            var parser = ConnectionStringParser<EmbeddedRavenConnectionStringOptions>.FromConnectionString(connectionString);
            parser.Parse();

            var options = parser.ConnectionStringOptions;
            var documentStore = new EmbeddableDocumentStore();

            if (options.ResourceManagerId != Guid.Empty)
            {
                documentStore.ResourceManagerId = options.ResourceManagerId;
            }

            if (options.Credentials != null)
            {
                documentStore.Credentials = options.Credentials;
            }

            if (string.IsNullOrEmpty(options.Url) == false)
            {
                documentStore.Url = options.Url;
            }

            if (string.IsNullOrEmpty(options.DefaultDatabase) == false)
            {
                documentStore.DefaultDatabase = options.DefaultDatabase;
            }

            if (string.IsNullOrEmpty(options.ApiKey) == false)
            {
                documentStore.ApiKey = options.ApiKey;
            }

            documentStore.EnlistInDistributedTransactions = options.EnlistInDistributedTransactions;

            if (string.IsNullOrEmpty(options.DataDirectory) == false)
            {
                documentStore.DataDirectory = options.DataDirectory;
            }

            documentStore.RunInMemory = options.RunInMemory;

            return documentStore;
        }
#endif
    }
}