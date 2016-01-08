using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Zuehlke.AppMonitor.Server.Api.Models;
using Zuehlke.AppMonitor.Server.DataAccess;
using Zuehlke.AppMonitor.Server.DataAccess.Entities;

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
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var projects = await this.dataAccess.Projects.GetList(new PagingQuery { Skip = 0, Top = 50 });

            return this.Ok(projects.Items);
        }

        // GET api/projects/5
        [HttpGet("{id}", Name = "GetProject")]
        public async Task<IActionResult> Get(string id)
        {
            var project = await this.dataAccess.Projects.Get(id);
            if (project == null)
            {
                return this.HttpNotFound();
            }

            return this.Ok(project);
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

            var entity = await this.dataAccess.Projects.Create(Mapper.Map<ProjectDto, Project>(item));

            var result = Mapper.Map<Project, ProjectDto>(entity);
            return this.CreatedAtRoute("GetProject", new { controller = "Projects", id = result.Name }, result);
        }

        // PUT api/projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]ProjectDto item)
        {
            if (item == null || item.Name != id)
            {
                return this.HttpBadRequest();
            }

            var project = await this.dataAccess.Projects.Get(id);
            if (project == null)
            {
                return this.HttpNotFound();
            }

            await this.dataAccess.Projects.Update(id, p => Mapper.Map(item, p));

            return new NoContentResult();
        }

        // DELETE api/projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await this.dataAccess.Projects.Delete(id);

            return new NoContentResult();
        }
    }
}