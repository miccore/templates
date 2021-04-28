using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sample.Microservice.Services.Sample.DomainModels;

namespace Sample.Microservice.Services.Sample {

    public interface ISampleService{


        Task<IEnumerable<SampleDomainModel>> GetAllSamplesAsync();

        Task<SampleDomainModel> GetSampleAsync(int id);

        Task<SampleDomainModel> CreateSampleAsync(SampleDomainModel Sample);

        Task<SampleDomainModel> UpdateSampleAsync(SampleDomainModel Sample);

        Task<int> DeleteSampleAsync(int id);

    }

}