using AutoMapper;
using Sample.Microservice.Operations.Sample.ViewModels;
using Sample.Microservice.Services.Sample.DomainModels;

namespace Sample.Microservice.Operations.Sample.MapperProfiles
{
    public class UpdateSampleRequestProfile : Profile
    {
        public UpdateSampleRequestProfile()
        {
            CreateMap<UpdateSampleViewModel, SampleDomainModel>().ReverseMap();
        }
    }
}
