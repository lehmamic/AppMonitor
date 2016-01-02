using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Zuehlke.AppMonitor.Server.Api.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Zuehlke.AppMonitor.Server.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        // GET: api/projects
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var projects = await Task.Factory.StartNew(() => new []
            {
                new Project()
            });

            return this.Ok(projects);
        }

        // GET api/projects/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var project = await Task.Factory.StartNew(() => new Project() );

            return this.Ok(project);
        }

        // POST api/projects
        [HttpPost]
        public void Post([FromBody]Project project)
        {
        }

        // PUT api/projects/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Project project)
        {
        }

        // DELETE api/projects/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}