using AutoMapper;

namespace Zuehlke.AppMonitor.Server.DataAccess.Mappings
{
    public class ProjectProfile : Profile
    {
        #region Overrides of Profile
        protected override void Configure()
        {
            this.CreateMap<Api.Models.ProjectDto, Entities.Project>().ForMember(dest => dest.Id, opt => opt.Ignore());
        }
        #endregion
    }
}
