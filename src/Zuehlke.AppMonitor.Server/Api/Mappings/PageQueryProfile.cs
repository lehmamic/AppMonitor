using AutoMapper;
using Zuehlke.AppMonitor.Server.Api.Models;
using Zuehlke.AppMonitor.Server.DataAccess.Entities;

namespace Zuehlke.AppMonitor.Server.Api.Mappings
{
    public class PageQueryProfile : Profile
    {
        #region Overrides of Profile
        protected override void Configure()
        {
            this.CreateMap<PageQueryDto, PagingQuery>();
        }
        #endregion
    }
}
