using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Miccore.Net.webapi_template.User.Api.Entities;
using  Miccore.Net.webapi_template.User.Api.Services.Role.DomainModels;

namespace  Miccore.Net.webapi_template.User.Api.Services.Role {

    public interface IRoleService{

        Task<RoleDomainModel> GetRoleAsync(int id);
        Task<RoleDomainModel> CreateRoleAsync(RoleDomainModel Role);
        Task<RoleDomainModel> UpdateRoleAsync(RoleDomainModel Role);
        Task<int> DeleteRoleAsync(int id);
        Task<PaginationEntity<RoleDomainModel>> GetAllRolesAsync(int page, int limit);
    }

}