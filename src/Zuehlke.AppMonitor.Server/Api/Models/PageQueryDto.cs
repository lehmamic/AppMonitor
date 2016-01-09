namespace Zuehlke.AppMonitor.Server.Api.Models
{
    public class PageQueryDto
    {
        public int Skip { get; set; }

        public int Top { get; set; } = 50;
    }
}
