using System;

namespace Zuehlke.AppMonitor.Server.Api.Models
{
    public class ProjectDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime LastModifiedAtUtc { get; set; }
    }
}