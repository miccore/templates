using AutoMapper;
using Miccore.Net.webapi_template.User.Api.Entities;
using  Miccore.Net.webapi_template.User.Api.Repositories.Role.DtoModels;
using  Miccore.Net.webapi_template.User.Api.Services.Role.DomainModels;

namespace  Miccore.Net.webapi_template.User.Api.Services.Role.MapperProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleDomainModel, RoleDtoModel>().ReverseMap();
            CreateMap<PaginationEntity<RoleDomainModel>, PaginationEntity<RoleDtoModel>>().ReverseMap();
        }
    }
}
