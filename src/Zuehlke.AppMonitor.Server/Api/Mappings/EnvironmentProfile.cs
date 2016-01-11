using AutoMapper;
using Zuehlke.AppMonitor.Server.Api.Models;
using Environment = Zuehlke.AppMonitor.Server.DataAccess.Entities.Environment;

namespace Zuehlke.AppMonitor.Server.Api.Mappings
{
    public class EnvironmentProfile : Profile
    {
        #region Overrides of Profile
        protected override void Configure()
        {
            this.CreateMap<EnvironmentDto, Environment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAtUtc, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedAtUtc, opt => opt.Ignore());

            this.CreateMap<Environment, EnvironmentDto>();
        }
        #endregion
    }
}
