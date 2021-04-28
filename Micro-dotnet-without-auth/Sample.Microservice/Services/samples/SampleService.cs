using System;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sample.Microservice.Services.Sample.DomainModels;
using Sample.Microservice.Repositories.Sample.DtoModels;
using Sample.Microservice.Services.Sample;
using Sample.Microservice.Repositories.Sample;


namespace Sample.Microservice.Services.Sample {

    public class SampleService : ISampleService {

        private readonly ISampleRepository _sampleRepository;
        private readonly IMapper _mapper;
        public SampleService(ISampleRepository SampleRepository, IMapper mapper) {
            _sampleRepository = SampleRepository;
            _mapper = mapper;
        }
        

        public async Task<SampleDomainModel> CreateSampleAsync(SampleDomainModel Sample)
        {
            var dto = await _sampleRepository.Create(_mapper.Map<SampleDtoModel>(Sample));
            return _mapper.Map<SampleDomainModel>(dto);
        }

        public async Task<int> DeleteSampleAsync(int id) => await _sampleRepository.DeleteAsync(id);

        public async Task<IEnumerable<SampleDomainModel>> GetAllSamplesAsync()
        {
            var samples = await _sampleRepository.GetAllAsync();
            return _mapper.Map<List<SampleDomainModel>>(samples);
        }

        public async Task<SampleDomainModel> GetSampleAsync(int id)
        {
            var sample = await _sampleRepository.GetSingleAsync(id);
            return _mapper.Map<SampleDomainModel>(sample);
        }

        public async Task<SampleDomainModel> UpdateSampleAsync(SampleDomainModel Sample)
        {
            var dto = await _sampleRepository.UpdateAsync(_mapper.Map<SampleDtoModel>(Sample));
            return _mapper.Map<SampleDomainModel>(dto);
        }
    }

}