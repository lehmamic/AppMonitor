using System.Collections.ObjectModel;

namespace Zuehlke.AppMonitor.Server.Api.Models
{
    public class PagingResult<T> : Collection<T>
    {
        public string NextPageLink { get; set; }


        public int TotalCount { get; set; }
    }
}