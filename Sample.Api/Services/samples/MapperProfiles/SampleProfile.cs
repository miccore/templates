using AutoMapper;
using Miccore.Net.webapi_template.Sample.Api.Repositories.Sample.DtoModels;
using Miccore.Net.webapi_template.Sample.Api.Services.Sample.DomainModels;

namespace Miccore.Net.webapi_template.Sample.Api.Services.Sample.MapperProfiles
{
    public class SampleProfile : Profile
    {
        public SampleProfile()
        {
            CreateMap<SampleDomainModel, SampleDtoModel>().ReverseMap();
        }
    }
}
