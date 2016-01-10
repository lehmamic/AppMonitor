using System;

namespace Zuehlke.AppMonitor.Server.DataAccess.Entities
{
    public class Environment : IEntity<Guid>
    {
        #region Implementation of IEntity<out Guid>
        public Guid Id { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? ModifiedAtUtc { get; set; }
        #endregion

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid ProjectId { get; set; }
    }
}
