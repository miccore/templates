using System;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using  Miccore.Net.webapi_template.User.Api.Services.Role.DomainModels;
using  Miccore.Net.webapi_template.User.Api.Repositories.Role.DtoModels;
using  Miccore.Net.webapi_template.User.Api.Services.Role;
using  Miccore.Net.webapi_template.User.Api.Repositories.Role;
using Miccore.Net.webapi_template.User.Api.Entities;

namespace  Miccore.Net.webapi_template.User.Api.Services.Role {

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

        public async Task<PaginationEntity<RoleDomainModel>> GetAllRolesAsync(int page, int limit)
        {
            var roles = await _roleRepository.GetAllAsync(page, limit);
            return _mapper.Map<PaginationEntity<RoleDomainModel>>(roles);
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