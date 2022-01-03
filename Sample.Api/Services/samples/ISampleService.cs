using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Miccore.Net.webapi_template.Sample.Api.Services.Sample.DomainModels;

namespace Miccore.Net.webapi_template.Sample.Api.Services.Sample {

    public interface ISampleService{


        Task<IEnumerable<SampleDomainModel>> GetAllSamplesAsync();

        Task<SampleDomainModel> GetSampleAsync(int id);

        Task<SampleDomainModel> CreateSampleAsync(SampleDomainModel Sample);

        Task<SampleDomainModel> UpdateSampleAsync(SampleDomainModel Sample);

        Task<int> DeleteSampleAsync(int id);

    }

}