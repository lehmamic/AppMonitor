using System;

namespace Zuehlke.AppMonitor.Server.DataAccess.Entities
{
    public class PagingResult<T>
    {
        public PagingResult(T[] items, long totalCount)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            this.Items = items;
            this.TotalCount = totalCount;
        }

        public T[] Items { get; }

        public long TotalCount { get; }
    }
}
