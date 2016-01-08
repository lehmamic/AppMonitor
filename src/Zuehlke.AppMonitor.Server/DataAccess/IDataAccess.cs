using Zuehlke.AppMonitor.Server.DataAccess.Entities;

namespace Zuehlke.AppMonitor.Server.DataAccess
{
    public interface IDataAccess
    {
        IRepository<Project, string> Projects { get; }
    }
}