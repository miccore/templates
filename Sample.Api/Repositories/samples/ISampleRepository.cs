using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Miccore.Net.webapi_template.Sample.Api.Repositories.Sample.DtoModels;

namespace Miccore.Net.webapi_template.Sample.Api.Repositories.Sample {

    public interface ISampleRepository{

        Task<IEnumerable<SampleDtoModel>> GetAllAsync();

        Task<SampleDtoModel> GetSingleAsync(int id);

        Task<SampleDtoModel> Create(SampleDtoModel sample);

        Task<SampleDtoModel> UpdateAsync(SampleDtoModel sample);

        Task<int> DeleteAsync(int id);

    }

}