using System;
using System.ComponentModel.DataAnnotations;

namespace Zuehlke.AppMonitor.Server.Api.Models
{
    public class ProjectDto : IDataTransferObject<Guid>
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? ModifiedAtUtc { get; set; }
    }
}