using AutoMapper;
using User.Microservice.Repositories.Role.DtoModels;
using User.Microservice.Services.Role.DomainModels;

namespace User.Microservice.Services.Role.MapperProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleDomainModel, RoleDtoModel>().ReverseMap();
        }
    }
}
