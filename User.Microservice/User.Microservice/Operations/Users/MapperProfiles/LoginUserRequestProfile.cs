using AutoMapper;
using User.Microservice.Operations.User.ViewModels;
using User.Microservice.Services.User.DomainModels;

namespace User.Microservice.Operations.User.MapperProfiles
{
    public class LoginUserRequestProfile : Profile
    {
        public LoginUserRequestProfile()
        {
            CreateMap<LoginUserViewModel, UserDomainModel>().ReverseMap();
        }
    }
}
