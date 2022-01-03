using AutoMapper;
using  Miccore.Net.webapi_template.User.Api.Repositories.User.DtoModels;
using  Miccore.Net.webapi_template.User.Api.Services.User.DomainModels;

namespace  Miccore.Net.webapi_template.User.Api.Services.User.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDomainModel, UserDtoModel>().ReverseMap();
        }
    }
}
