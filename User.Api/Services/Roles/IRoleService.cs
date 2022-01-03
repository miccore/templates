using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using  Miccore.Net.webapi_template.User.Api.Services.Role.DomainModels;

namespace  Miccore.Net.webapi_template.User.Api.Services.Role {

    public interface IRoleService{


        Task<IEnumerable<RoleDomainModel>> GetAllRolesAsync();
        Task<RoleDomainModel> GetRoleAsync(int id);
        Task<RoleDomainModel> CreateRoleAsync(RoleDomainModel Role);
        Task<RoleDomainModel> UpdateRoleAsync(RoleDomainModel Role);
        Task<int> DeleteRoleAsync(int id);

    }

}