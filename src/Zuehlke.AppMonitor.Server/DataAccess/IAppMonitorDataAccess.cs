namespace Zuehlke.AppMonitor.Server.DataAccess
{
    public interface IAppMonitorDataAccess
    {
        IProjectsRepository Projects { get; }
    }
}