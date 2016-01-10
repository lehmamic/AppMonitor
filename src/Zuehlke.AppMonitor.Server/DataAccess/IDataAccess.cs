using System;
using Zuehlke.AppMonitor.Server.DataAccess.Entities;
using Environment = Zuehlke.AppMonitor.Server.DataAccess.Entities.Environment;

namespace Zuehlke.AppMonitor.Server.DataAccess
{
    public interface IDataAccess
    {
        IRepository<Project, Guid> Projects { get; }

        IRepositoryCollection<Environment, Guid> Environments { get; }
    }
}