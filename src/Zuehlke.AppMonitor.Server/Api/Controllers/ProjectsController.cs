using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Zuehlke.AppMonitor.Server.Api.Models;
using Zuehlke.AppMonitor.Server.DataAccess;
using Zuehlke.AppMonitor.Server.DataAccess.Entities;
using Zuehlke.AppMonitor.Server.Utils.Projection;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Zuehlke.AppMonitor.Server.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        private readonly IDataAccess dataAccess;

        public ProjectsController(IDataAccess dataAccess)
        {
            if (dataAccess == null)
            {
                throw new ArgumentNullException(nameof(dataAccess));
            }

            this.dataAccess = dataAccess;
        }

        // GET: api/projects
        [HttpGet(Name = "GetProjectList")]
        public async Task<IActionResult> Get([FromQuery]PageQueryDto<ProjectDto> pageQuery)
        {
            PageResultDto<ProjectDto> result = await this.dataAccess.Projects.GetListAsync(pageQuery);
            result.NextPageLink = this.NextPageLink("GetProjectList", "Projects", pageQuery);

            return this.Ok(result);
        }

        // GET api/projects/5
        [HttpGet("{id}", Name = "GetProject")]
        public async Task<IActionResult> Get(Guid id)
        {
            Project project = await this.dataAccess.Projects.GetAsync(id);
            if (project == null)
            {
                return this.HttpNotFound();
            }

            return this.Ok(project.ProjectedAs<ProjectDto>());
        }

        // POST api/projects
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProjectDto item)
        {
            if (item == null)
            {
                return this.HttpBadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.HttpBadRequest();
            }

            ProjectDto result = await this.dataAccess.Projects.Create(item);

            return this.CreatedAtRoute("GetProject", new { controller = "Projects", id = result.Id }, result);
        }

        // PUT api/projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]ProjectDto item)
        {
            if (item == null || item.Id != id)
            {
                return this.HttpBadRequest();
            }

            var project = await this.dataAccess.Projects.GetAsync(id);
            if (project == null)
            {
                return this.HttpNotFound();
            }

            await this.dataAccess.Projects.Update(id, item);

            return new NoContentResult();
        }

        // DELETE api/projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this.dataAccess.Projects.DeleteAsync(id);

            return new NoContentResult();
        }
    }
}