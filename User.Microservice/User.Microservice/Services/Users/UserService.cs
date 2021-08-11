using System;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.Microservice.Services.User.DomainModels;
using User.Microservice.Repositories.User.DtoModels;
using User.Microservice.Services.User;
using User.Microservice.Repositories.User;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using User.Microservice.Services.Role;
using System.Security.Cryptography;

namespace User.Microservice.Services.User {

    public class UserService : IUserService {

        private readonly IUserRepository _userRepository;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        
        public UserService(IUserRepository UserRepository, IMapper mapper, IConfiguration config, IRoleService roleService) {
            _userRepository = UserRepository;
            _roleService = roleService;
            _mapper = mapper;
            _config = config;
        }
        
        public async Task<UserDomainModel> LoginUserAsync(UserDomainModel User)
        {
            var getUser = await _userRepository.AuthenticateUser(_mapper.Map<UserDtoModel>(User));
            if(getUser == null){
                return _mapper.Map<UserDomainModel>(getUser);
            }
            var role = await _roleService.GetRoleAsync(getUser.RoleId);
            var dUser = _mapper.Map<UserDomainModel>(getUser);
            dUser.Role = role;
            var token = GenerateToken(dUser);
            dUser.Token = token;var refreshToken = GenerateRefreshToken();

            getUser.RefreshToken = refreshToken;
            await _userRepository.UpdateAsync(getUser);

            return dUser;

        }

        public async Task<UserDomainModel> CreateUserAsync(UserDomainModel User)
        {
            var dto = await _userRepository.Create(_mapper.Map<UserDtoModel>(User));
            return _mapper.Map<UserDomainModel>(dto);
        }

        public async Task<int> DeleteUserAsync(int id) => await _userRepository.DeleteAsync(id);


        public async Task<IEnumerable<UserDomainModel>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<List<UserDomainModel>>(users);
        }

        public async Task<UserDomainModel> GetUserAsync(int id)
        {
            var user = await _userRepository.GetSingleAsync(id);
            return _mapper.Map<UserDomainModel>(user);
        }

        public async Task<UserDomainModel> GetUserByPhoneAsync(string phone)
        {
            var user = await _userRepository.GetSingleByPhoneAsync(phone);
            return _mapper.Map<UserDomainModel>(user);
        }

        public async Task<UserDomainModel> GetUserByEmailAsync(string Email)
        {
            var user = await _userRepository.GetSingleByEmailAsync(Email);
            return _mapper.Map<UserDomainModel>(user);
        }

        public async Task<UserDomainModel> GetSingleByRefreshTokenAsync(string refresh)
        {
            var dto = await _userRepository.GetSingleByRefreshTokenAsync(refresh);
            return _mapper.Map<UserDomainModel>(dto);
        }


        public async Task<UserDomainModel> UpdateUserAsync(UserDomainModel User)
        {
            var dto = await _userRepository.UpdateAsync(_mapper.Map<UserDtoModel>(User));
            return _mapper.Map<UserDomainModel>(dto);
        }

        public async Task<UserDomainModel> UpdatePasswordAsync(int id, string oldpassword, string newpassword)
        {
            var dto = await _userRepository.UpdatePasswordAsync(id, oldpassword, newpassword);
            return _mapper.Map<UserDomainModel>(dto);
        }


        public string GenerateToken(UserDomainModel User)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("322e9998-f1f0-494a-9b9d-aea4e0008888"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var guid = Guid.NewGuid().ToString();
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, User.FirstName),
                new Claim("fullName", User.FirstName),
                new Claim("role",User.Role.Name),
                new Claim(JwtRegisteredClaimNames.Jti, guid)
            };
            var token = new JwtSecurityToken(
                                            issuer: "http://localhost:44373/",
                                            audience: "http://localhost:44373/",
                                            claims: claims,
                                            expires: DateTime.Now.AddMinutes(3600),
                                            signingCredentials: credentials
                                            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }

}