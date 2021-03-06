﻿using System;
using Microsoft.AspNet.Mvc;

namespace Zuehlke.AppMonitor.Server.Api.Models
{
    public static class PagingExtensions
    {
        public static PageQueryDto<T> ValueOrDefault<T>(this PageQueryDto<T> query)
        {
            return query ?? new PageQueryDto<T> { Skip = 0, Top = 50 };
        }

        public static string NextPageLink<T>(this Controller controller, string controllerName, string routeName, PageQueryDto<T> query)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            if (controllerName == null)
            {
                throw new ArgumentNullException(nameof(controllerName));
            }

            if (routeName == null)
            {
                throw new ArgumentNullException(nameof(routeName));
            }

            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            query = query.ValueOrDefault();

            return controller.Url.Link(routeName, new { controller = controllerName, skip = query.Skip + query.Top, top = query.Top });
        }
    }
}
