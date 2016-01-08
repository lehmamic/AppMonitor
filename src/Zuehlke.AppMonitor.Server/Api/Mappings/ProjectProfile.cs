using AutoMapper;
using Zuehlke.AppMonitor.Server.Api.Models;
using Zuehlke.AppMonitor.Server.DataAccess.Entities;

namespace Zuehlke.AppMonitor.Server.Api.Mappings
{
    public class ProjectProfile : Profile
    {
        #region Overrides of Profile
        protected override void Configure()
        {
            this.CreateMap<ProjectDto, Project>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAtUtc, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedAtUtc, opt => opt.Ignore());

            this.CreateMap<Project, ProjectDto>();
        }
        #endregion
    }
}
