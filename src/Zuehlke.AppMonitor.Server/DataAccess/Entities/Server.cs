using System;
using System.Collections.Generic;

namespace Zuehlke.AppMonitor.Server.DataAccess.Entities
{
    public class Server : IEntity<Guid>
    {
        #region Implementation of IEntity<out Guid>
        public Guid Id { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? ModifiedAtUtc { get; set; }
        #endregion

        public string Name { get; set; }

        public string Description { get; set; }

        public string RootUrl { get; set; }

        public ICollection<string> Roles { get; } = new List<string>();

        public Guid ProjectId { get; set; }

        public Guid EnvironmentId { get; set; }
    }
}