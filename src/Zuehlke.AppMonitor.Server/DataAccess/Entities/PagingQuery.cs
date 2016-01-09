namespace Zuehlke.AppMonitor.Server.DataAccess.Entities
{
    public class PagingQuery<T>
    {
        public int Skip { get; set; }

        public int Top { get; set; }
    }
}