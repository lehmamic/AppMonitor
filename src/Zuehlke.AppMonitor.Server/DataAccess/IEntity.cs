using System;

namespace Zuehlke.AppMonitor.Server.DataAccess
{
    public interface IEntity<out TId>
    {
        TId Id { get; }

        DateTime CreatedAtUtc { get; set; }

        DateTime? ModifiedAtUtc { get; set; }
    }
}