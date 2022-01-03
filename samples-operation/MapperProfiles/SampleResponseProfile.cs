using AutoMapper;
using Miccore.Net.webapi_template.Sample.Api.Operations.Sample.ViewModels;
using Miccore.Net.webapi_template.Sample.Api.Services.Sample.DomainModels;

namespace Miccore.Net.webapi_template.Sample.Api.Operations.Sample.MapperProfiles
{
    public class SampleResponseProfile : Profile
    {
        public SampleResponseProfile()
        {
            CreateMap<SampleViewModel, SampleDomainModel>().ReverseMap();
        }
    }
}
