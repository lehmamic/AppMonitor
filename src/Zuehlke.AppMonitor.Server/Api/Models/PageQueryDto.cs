namespace Zuehlke.AppMonitor.Server.Api.Models
{
    public class PageQueryDto<T>
    {
        public int Skip { get; set; } = 0;

        public int Top { get; set; } = 50;
    }
}
