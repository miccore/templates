using AutoMapper;
using  Miccore.Net.webapi_template.User.Api.Operations.User.ViewModels;
using  Miccore.Net.webapi_template.User.Api.Services.User.DomainModels;

namespace  Miccore.Net.webapi_template.User.Api.Operations.User.MapperProfiles
{
    public class LoginUserRequestProfile : Profile
    {
        public LoginUserRequestProfile()
        {
            CreateMap<LoginUserViewModel, UserDomainModel>().ReverseMap();
        }
    }
}
