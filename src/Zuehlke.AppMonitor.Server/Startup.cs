using System.Linq;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Zuehlke.AppMonitor.Server.Api.Mappings;
using Zuehlke.AppMonitor.Server.DataAccess.Raven;
using Zuehlke.AppMonitor.Server.Utils;

namespace Zuehlke.AppMonitor.Server
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            this.Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInstance(this.Configuration);

            services.AddRavenDbDataAccess(this.Configuration);

            // Add framework services.
            services.AddMvc(options =>
            {
                options.OutputFormatters.OfType<JsonOutputFormatter>().First().SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseAutoMapper(c =>
            {
                c.AddProfile<ProjectProfile>();
                c.AddProfile<PageQueryProfile>();
                c.AddProfile<PageResultProfile>();
            });

            app.UseIISPlatformHandler();

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
