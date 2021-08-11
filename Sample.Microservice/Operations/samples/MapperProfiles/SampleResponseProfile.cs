using AutoMapper;
using Sample.Microservice.Operations.Sample.ViewModels;
using Sample.Microservice.Services.Sample.DomainModels;

namespace Sample.Microservice.Operations.Sample.MapperProfiles
{
    public class SampleResponseProfile : Profile
    {
        public SampleResponseProfile()
        {
            CreateMap<SampleViewModel, SampleDomainModel>().ReverseMap();
        }
    }
}
