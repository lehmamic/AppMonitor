﻿namespace Zuehlke.AppMonitor.Server.Api.Models
{
    public class PagingQueryDto
    {
        public int Page { get; set; }

        public int PageSize { get; set; } = 50;
    }
}