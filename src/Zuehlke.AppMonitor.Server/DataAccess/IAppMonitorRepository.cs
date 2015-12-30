using System.Threading.Tasks;
using Zuehlke.AppMonitor.Server.Api.Models;

namespace Zuehlke.AppMonitor.Server.DataAccess
{
    public interface IProjectsRepository
    {
        Task<PagingResult<Project>> Get(int page, int pageSize);

        Task<Project> Get(int id);

        Task Create(Project project);

        Task Update(Project project);

        Task Delete(Project project);
    }
}