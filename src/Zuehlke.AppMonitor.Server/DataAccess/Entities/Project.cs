using System;

namespace Zuehlke.AppMonitor.Server.DataAccess.Entities
{
    public class Project : IEntity<string>
    {
        public string Id => this.Name;

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? ModifiedAtUtc { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
