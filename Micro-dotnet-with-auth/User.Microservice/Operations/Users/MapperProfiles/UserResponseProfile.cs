using AutoMapper;
using User.Microservice.Operations.User.ViewModels;
using User.Microservice.Services.User.DomainModels;

namespace User.Microservice.Operations.User.MapperProfiles
{
    public class UserResponseProfile : Profile
    {
        public UserResponseProfile()
        {
            CreateMap<UserViewModel, UserDomainModel>().ReverseMap();
        }
    }
}
