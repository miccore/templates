using AutoMapper;
using Miccore.Net.webapi_template.User.Api.Entities;
using  Miccore.Net.webapi_template.User.Api.Operations.User.ViewModels;
using  Miccore.Net.webapi_template.User.Api.Services.User.DomainModels;

namespace  Miccore.Net.webapi_template.User.Api.Operations.User.MapperProfiles
{
    public class UserResponseProfile : Profile
    {
        public UserResponseProfile()
        {
            CreateMap<UserViewModel, UserDomainModel>().ReverseMap();
            CreateMap<PaginationEntity<UserViewModel>, PaginationEntity<UserDomainModel>>().ReverseMap();
        }
    }
}
