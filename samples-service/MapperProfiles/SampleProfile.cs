using AutoMapper;
using Miccore.Net.webapi_template.Sample.Api.Repositories.Sample.DtoModels;
using Miccore.Net.webapi_template.Sample.Api.Services.Sample.DomainModels;
using Miccore.Net.webapi_template.Sample.Api.Entities;

namespace Miccore.Net.webapi_template.Sample.Api.Services.Sample.MapperProfiles
{
    public class SampleProfile : Profile
    {
        public SampleProfile()
        {
            CreateMap<SampleDomainModel, SampleDtoModel>().ReverseMap();
            CreateMap<PaginationEntity<SampleDomainModel>, PaginationEntity<SampleDtoModel>>().ReverseMap();
        }
    }
}
