using AutoMapper;
using Sample.Microservice.Repositories.Sample.DtoModels;
using Sample.Microservice.Services.Sample.DomainModels;

namespace Sample.Microservice.Services.Sample.MapperProfiles
{
    public class SampleProfile : Profile
    {
        public SampleProfile()
        {
            CreateMap<SampleDomainModel, SampleDtoModel>().ReverseMap();
        }
    }
}
