using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Miccore.Net.webapi_template.Sample.Api.Repositories.Sample.DtoModels;
using Miccore.Net.webapi_template.Sample.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Miccore.Net.webapi_template.Sample.Api.Repositories.Sample {

    public class SampleRepository : ISampleRepository {
        private readonly IApplicationDbContext _context;
        
        public SampleRepository(
            IApplicationDbContext context
        ){
            _context = context;
        }

        public async Task<SampleDtoModel> Create(SampleDtoModel sample)
        {
           await _context.Samples.AddAsync(sample);
           await _context.SaveChanges();

           return sample;
        }

        public async Task<int> DeleteAsync(int id)
        {
           var dto = _context.Samples.FirstOrDefault(x => x.Id == id);
            _context.Samples.Remove(dto);
            await _context.SaveChanges();

            return id;
        }

        public async Task<IEnumerable<SampleDtoModel>> GetAllAsync()
        {
            var samples = await _context.Samples.ToListAsync();
            
            return samples;
        }

        public async Task<SampleDtoModel> GetSingleAsync(int id)
        {
            var sample =  await _context.Samples.FirstOrDefaultAsync(x => x.Id == id);
            return sample;
        }

        public async Task<SampleDtoModel> UpdateAsync(SampleDtoModel sample)
        {
            Contract.Requires(sample != null);
            var dto = await _context.Samples.FirstOrDefaultAsync(x => x.Id == sample.Id);
            dto = sample;
            await _context.SaveChanges();

            return sample;
        }
    }

}
