using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zuehlke.AppMonitor.Server.Api.Models
{
    public class EnvironmentDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? ModifiedAtUtc { get; set; }
    }
}
