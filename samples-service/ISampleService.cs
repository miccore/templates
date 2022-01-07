using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Miccore.Net.webapi_template.Sample.Api.Services.Sample.DomainModels;
using Miccore.Net.webapi_template.Sample.Api.Entities;

namespace Miccore.Net.webapi_template.Sample.Api.Services.Sample {

    public interface ISampleService{


        Task<PaginationEntity<SampleDomainModel>> GetAllSamplesAsync(int page, int limit);

        Task<SampleDomainModel> GetSampleAsync(int id);

        Task<SampleDomainModel> CreateSampleAsync(SampleDomainModel Sample);

        Task<SampleDomainModel> UpdateSampleAsync(SampleDomainModel Sample);

        Task<int> DeleteSampleAsync(int id);

    }

}