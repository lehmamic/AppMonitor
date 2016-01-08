using System;
using System.ComponentModel.DataAnnotations;

namespace Zuehlke.AppMonitor.Server.Api.Models
{
    public class ProjectDto
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedAtUtc { get; set; }

        public DateTime? ModifiedAtUtc { get; set; }
    }
}