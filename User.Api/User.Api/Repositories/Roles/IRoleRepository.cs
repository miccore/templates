using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Miccore.Net.webapi_template.User.Api.Repositories.Role.DtoModels;

namespace Miccore.Net.webapi_template.User.Api.Repositories.Role {

    public interface IRoleRepository{

        Task<IEnumerable<RoleDtoModel>> GetAllAsync();

        Task<RoleDtoModel> GetSingleAsync(int id);

        Task<RoleDtoModel> Create(RoleDtoModel role);

        Task<RoleDtoModel> UpdateAsync(RoleDtoModel role);

        Task<int> DeleteAsync(int id);

    }

}