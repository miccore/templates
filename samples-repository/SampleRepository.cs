using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Miccore.Net.webapi_template.Sample.Api.Repositories.Sample.DtoModels;
using Miccore.Net.webapi_template.Sample.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Miccore.Net.webapi_template.Sample.Api.Entities;

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
            sample.CreatedAt = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 01, 01, 0, 0, 0)).TotalSeconds;
            await _context.Samples.AddAsync(sample);
            await _context.SaveChanges();

           return sample;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var dto = _context.Samples.FirstOrDefault(x => x.Id == id);
            dto.DeletedAt = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 01, 01, 0, 0, 0)).TotalSeconds;
            await _context.SaveChanges();

            return id;
        }

        public async Task<PaginationEntity<SampleDtoModel>> GetAllAsync(int page, int limit)
        {
            var samples = await _context.Samples
                                        .Where(x => x.DeletedAt == null)
                                        .OrderBy(x => x.CreatedAt) 
                                        .PaginateAsync(page, limit);
            
            return samples;
        }

        public async Task<SampleDtoModel> GetSingleAsync(int id)
        {
            var sample =  await _context.Samples.FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);
            return sample;
        }

        public async Task<SampleDtoModel> UpdateAsync(SampleDtoModel sample)
        {
            Contract.Requires(sample != null);
            var dto = await _context.Samples.FirstOrDefaultAsync(x => x.Id == sample.Id && x.DeletedAt == null);
            dto = sample;
            dto.UpdatedAt = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 01, 01, 0, 0, 0)).TotalSeconds;
            await _context.SaveChanges();

            return sample;
        }
    }

}
