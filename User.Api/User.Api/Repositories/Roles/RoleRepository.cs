using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Miccore.Net.webapi_template.User.Api.Repositories.Role.DtoModels;
using Miccore.Net.webapi_template.User.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Miccore.Net.webapi_template.User.Api.Repositories.Role {

    public class RoleRepository : IRoleRepository {
        private readonly IApplicationDbContext _context;
        
        public RoleRepository(
            IApplicationDbContext context
        ){
            _context = context;
        }

        public async Task<RoleDtoModel> Create(RoleDtoModel role)
        {
            role.CreatedAt = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 01, 01, 0, 0, 0)).TotalSeconds;
            await _context.Roles.AddAsync(role);
            await _context.SaveChanges();

           return role;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var dto = _context.Roles.FirstOrDefault(x => x.Id == id);
            dto.DeletedAt = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 01, 01, 0, 0, 0)).TotalSeconds;
            await _context.SaveChanges();

            return id;
        }

        public async Task<IEnumerable<RoleDtoModel>> GetAllAsync()
        {
            var roles = await _context.Roles
                                    .Where(x => x.DeletedAt != null)
                                    .ToListAsync();
            
            return roles;
        }

        public async Task<RoleDtoModel> GetSingleAsync(int id)
        {
            var role =  await _context.Roles.FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt != null);
            return role;
        }

        public async Task<RoleDtoModel> UpdateAsync(RoleDtoModel role)
        {
            Contract.Requires(role != null);
            var dto = await _context.Roles.FirstOrDefaultAsync(x => x.Id == role.Id && x.DeletedAt != null);
            if(dto == null){
                return null;
            }
            dto = role;
            dto.UpdatedAt = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 01, 01, 0, 0, 0)).TotalSeconds;
            await _context.SaveChanges();

            return role;
        }
    }

}
