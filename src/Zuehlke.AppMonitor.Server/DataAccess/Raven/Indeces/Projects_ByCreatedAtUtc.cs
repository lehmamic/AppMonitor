using System.Linq;
using Raven.Client.Indexes;
using Zuehlke.AppMonitor.Server.DataAccess.Entities;

namespace Zuehlke.AppMonitor.Server.DataAccess.Raven.Indeces
{
    public class Projects_ByCreatedAtUtc : AbstractIndexCreationTask<Project>
    {
        public Projects_ByCreatedAtUtc()
        {
            this.Map = projects => from project in projects
                               select new
                               {
                                   CreatedAtUtc = project.CreatedAtUtc
                               };
        }
    }
}
