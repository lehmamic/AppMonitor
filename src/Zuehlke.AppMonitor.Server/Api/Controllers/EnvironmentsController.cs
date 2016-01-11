using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Zuehlke.AppMonitor.Server.Api.Models;
using Zuehlke.AppMonitor.Server.DataAccess;
using Zuehlke.AppMonitor.Server.Utils.Projection;
using Environment = Zuehlke.AppMonitor.Server.DataAccess.Entities.Environment;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Zuehlke.AppMonitor.Server.Api.Controllers
{
    [Route("api/projects/{project}/[controller]")]
    public class EnvironmentsController : Controller
    {
        private readonly IDataAccess dataAccess;

        public EnvironmentsController(IDataAccess dataAccess)
        {
            if (dataAccess == null)
            {
                throw new ArgumentNullException(nameof(dataAccess));
            }

            this.dataAccess = dataAccess;
        }

        // GET: api/projects/{project}/environments?skip=0&top=50
        [HttpGet(Name = "GetEnvironmentList")]
        public async Task<IActionResult> Get([FromRoute]Guid project, [FromQuery]PageQueryDto<EnvironmentDto> pageQuery)
        {
            IRepository<Environment, Guid> repository = await this.dataAccess.Environments.GetAsync(project);
            if (repository == null)
            {
                return this.HttpNotFound(project);
            }

            PageResultDto<EnvironmentDto> result = await repository.GetListAsync(pageQuery);
            result.NextPageLink = this.NextPageLink("Environments", "GetEnvironmentList", pageQuery);

            return this.Ok(result);
        }

        // GET api/projects/{project}/environments/5
        [HttpGet("{id}", Name = "GetEnvironment")]
        public async Task<IActionResult> Get([FromRoute]Guid project, Guid id)
        {
            IRepository<Environment, Guid> repository = await this.dataAccess.Environments.GetAsync(project);
            if (repository == null)
            {
                return this.HttpNotFound(project);
            }

            var environment = await repository.GetAsync(id);
            if (environment == null)
            {
                return this.HttpNotFound();
            }

            return this.Ok(environment.ProjectedAs<EnvironmentDto>());
        }

        // POST api/projects/{project}/environments/
        [HttpPost]
        public async Task<IActionResult> Post([FromRoute]Guid project, [FromBody]EnvironmentDto item)
        {
            if (item == null)
            {
                return this.HttpBadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.HttpBadRequest();
            }

            IRepository<Environment, Guid> repository = await this.dataAccess.Environments.GetAsync(project);
            if (repository == null)
            {
                return this.HttpNotFound(project);
            }

            EnvironmentDto result = await repository.Create(item);

            return this.CreatedAtRoute("GetEnvironment", new { controller = "Environments", id = result.Id }, result);
        }

        // PUT api/projects/{project}/environments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]Guid project, Guid id, [FromBody]EnvironmentDto item)
        {
            if (item == null || item.Id != id)
            {
                return this.HttpBadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.HttpBadRequest();
            }

            IRepository<Environment, Guid> repository = await this.dataAccess.Environments.GetAsync(project);
            if (repository == null)
            {
                return this.HttpNotFound(project);
            }

            Environment environment = await repository.GetAsync(id);
            if (environment == null)
            {
                return this.HttpNotFound();
            }

            await repository.Update(id, item);

            return new NoContentResult();
        }

        // DELETE api/projects/{project}/environments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid project, Guid id)
        {
            IRepository<Environment, Guid> repository = await this.dataAccess.Environments.GetAsync(project);
            if (repository == null)
            {
                return this.HttpNotFound(project);
            }

            await repository.DeleteAsync(id);

            return new NoContentResult();
        }
    }
}
