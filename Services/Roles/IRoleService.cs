using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.Microservice.Services.Role.DomainModels;

namespace User.Microservice.Services.Role {

    public interface IRoleService{


        Task<IEnumerable<RoleDomainModel>> GetAllRolesAsync();
        Task<RoleDomainModel> GetRoleAsync(int id);
        Task<RoleDomainModel> CreateRoleAsync(RoleDomainModel Role);
        Task<RoleDomainModel> UpdateRoleAsync(RoleDomainModel Role);
        Task<int> DeleteRoleAsync(int id);

    }

}