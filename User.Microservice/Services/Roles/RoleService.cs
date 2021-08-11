using System;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.Microservice.Services.Role.DomainModels;
using User.Microservice.Repositories.Role.DtoModels;
using User.Microservice.Services.Role;
using User.Microservice.Repositories.Role;


namespace User.Microservice.Services.Role {

    public class RoleService : IRoleService {

        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository RoleRepository, IMapper mapper) {
            _roleRepository = RoleRepository;
            _mapper = mapper;
        }
        

        public async Task<RoleDomainModel> CreateRoleAsync(RoleDomainModel Role)
        {
            var dto = await _roleRepository.Create(_mapper.Map<RoleDtoModel>(Role));
            return _mapper.Map<RoleDomainModel>(dto);
        }

        public async Task<int> DeleteRoleAsync(int id) => await _roleRepository.DeleteAsync(id);

        public async Task<IEnumerable<RoleDomainModel>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            return _mapper.Map<List<RoleDomainModel>>(roles);
        }

        public async Task<RoleDomainModel> GetRoleAsync(int id)
        {
            var role = await _roleRepository.GetSingleAsync(id);
            return _mapper.Map<RoleDomainModel>(role);
        }

        public async Task<RoleDomainModel> UpdateRoleAsync(RoleDomainModel Role)
        {
            var dto = await _roleRepository.UpdateAsync(_mapper.Map<RoleDtoModel>(Role));
            return _mapper.Map<RoleDomainModel>(dto);
        }
    }

}