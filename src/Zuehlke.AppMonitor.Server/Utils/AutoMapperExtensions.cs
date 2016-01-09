using System;
using AutoMapper;
using Microsoft.AspNet.Builder;
using Zuehlke.AppMonitor.Server.Utils.Projection;

namespace Zuehlke.AppMonitor.Server.Utils
{
    public static class AutoMapperExtensions
    {
        public static IApplicationBuilder UseAutoMapper(this IApplicationBuilder app, Action<IConfiguration> configure)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            Mapper.Initialize(configure);
            Mapper.AssertConfigurationIsValid();

            TypeAdapterFactory.SetCurrent(new AutoMapperTypeAdapterFactory());

            return app;
        }
    }
}
