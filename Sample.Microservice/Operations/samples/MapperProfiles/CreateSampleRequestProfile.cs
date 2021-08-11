using AutoMapper;
using Sample.Microservice.Operations.Sample.ViewModels;
using Sample.Microservice.Services.Sample.DomainModels;

namespace Sample.Microservice.Operations.Sample.MapperProfiles
{
    public class CreateSampleRequestProfile : Profile
    {
        public CreateSampleRequestProfile()
        {
            CreateMap<CreateSampleViewModel, SampleDomainModel>().ReverseMap();
        }
    }
}
