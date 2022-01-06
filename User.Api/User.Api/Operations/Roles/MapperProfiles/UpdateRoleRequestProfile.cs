using AutoMapper;
using  Miccore.Net.webapi_template.User.Api.Operations.Role.ViewModels;
using  Miccore.Net.webapi_template.User.Api.Services.Role.DomainModels;

namespace  Miccore.Net.webapi_template.User.Api.Operations.Role.MapperProfiles
{
    public class UpdateRoleRequestProfile : Profile
    {
        public UpdateRoleRequestProfile()
        {
            CreateMap<UpdateRoleViewModel, RoleDomainModel>().ReverseMap();
        }
    }
}
