using AutoMapper;
using User.Microservice.Repositories.User.DtoModels;
using User.Microservice.Services.User.DomainModels;

namespace User.Microservice.Services.User.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDomainModel, UserDtoModel>().ReverseMap();
        }
    }
}
