namespace Zuehlke.AppMonitor.Server.Api.Models
{
    public class PagingResultDto<T>
    {
        public T[] Items { get; set; }


        public string NextPageLink { get; set; }


        public int TotalCount { get; set; }
    }
}