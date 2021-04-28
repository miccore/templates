using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using User.Microservice.Repositories.Role.DtoModels;
using User.Microservice.Data;
using Microsoft.EntityFrameworkCore;

namespace User.Microservice.Repositories.Role {

    public class RoleRepository : IRoleRepository {
        private readonly IApplicationDbContext _context;
        
        public RoleRepository(
            IApplicationDbContext context
        ){
            _context = context;
        }

        public async Task<RoleDtoModel> Create(RoleDtoModel role)
        {
           await _context.Roles.AddAsync(role);
           await _context.SaveChanges();

           return role;
        }

        public async Task<int> DeleteAsync(int id)
        {
           var dto = _context.Roles.FirstOrDefault(x => x.Id == id);
            _context.Roles.Remove(dto);
            await _context.SaveChanges();

            return id;
        }

        public async Task<IEnumerable<RoleDtoModel>> GetAllAsync()
        {
            var roles = await _context.Roles.ToListAsync();
            
            return roles;
        }

        public async Task<RoleDtoModel> GetSingleAsync(int id)
        {
            var role =  await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            return role;
        }

        public async Task<RoleDtoModel> UpdateAsync(RoleDtoModel role)
        {
            Contract.Requires(role != null);
            var dto = await _context.Roles.FirstOrDefaultAsync(x => x.Id == role.Id);
            dto = role;
            await _context.SaveChanges();

            return role;
        }
    }

}
