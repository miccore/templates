using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.Microservice.Repositories.Role.DtoModels;

namespace User.Microservice.Repositories.Role {

    public interface IRoleRepository{

        Task<IEnumerable<RoleDtoModel>> GetAllAsync();

        Task<RoleDtoModel> GetSingleAsync(int id);

        Task<RoleDtoModel> Create(RoleDtoModel role);

        Task<RoleDtoModel> UpdateAsync(RoleDtoModel role);

        Task<int> DeleteAsync(int id);

    }

}