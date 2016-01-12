using System.Linq;
using Raven.Client.Indexes;
using Zuehlke.AppMonitor.Server.DataAccess.Entities;

namespace Zuehlke.AppMonitor.Server.DataAccess.Raven.Indeces
{
    public class Environments_ByProjectIdAndCreatedAtUtc : AbstractIndexCreationTask<Environment>
    {
        public Environments_ByProjectIdAndCreatedAtUtc()
        {
            this.Map = environments => from env in environments
                select new
                {
                    ProjectId = env.ProjectId,
                    CreatedAtUtc = env.CreatedAtUtc
                };
        }
    }
}