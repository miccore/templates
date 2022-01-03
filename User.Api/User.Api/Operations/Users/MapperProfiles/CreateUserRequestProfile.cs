using AutoMapper;
using Miccore.Net.webapi_template.User.Api.Operations.User.ViewModels;
using Miccore.Net.webapi_template.User.Api.Services.User.DomainModels;

namespace Miccore.Net.webapi_template.User.Api.Operations.User.MapperProfiles
{
    public class CreateUserRequestProfile : Profile
    {
        public CreateUserRequestProfile()
        {
            CreateMap<CreateUserViewModel, UserDomainModel>().ReverseMap();
        }
    }
}
