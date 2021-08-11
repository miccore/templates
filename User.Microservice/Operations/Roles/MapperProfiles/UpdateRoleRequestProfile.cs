using AutoMapper;
using User.Microservice.Operations.Role.ViewModels;
using User.Microservice.Services.Role.DomainModels;

namespace User.Microservice.Operations.Role.MapperProfiles
{
    public class UpdateRoleRequestProfile : Profile
    {
        public UpdateRoleRequestProfile()
        {
            CreateMap<UpdateRoleViewModel, RoleDomainModel>().ReverseMap();
        }
    }
}
