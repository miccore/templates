using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Miccore.Net.webapi_template.User.Api.Entities;
using  Miccore.Net.webapi_template.User.Api.Services.User.DomainModels;

namespace  Miccore.Net.webapi_template.User.Api.Services.User {

    public interface IUserService{

        Task<UserDomainModel> GetUserAsync(int id);
        Task<UserDomainModel> LoginUserAsync(UserDomainModel User);
        Task<UserDomainModel> CreateUserAsync(UserDomainModel User);
        Task<UserDomainModel> UpdateUserAsync(UserDomainModel User);
        Task<UserDomainModel> UpdatePasswordAsync(int id, string oldpassword, string newpassword);
        Task<int> DeleteUserAsync(int id);
        String GenerateToken(UserDomainModel User);
        string GenerateRefreshToken();
        Task<UserDomainModel> GetSingleByRefreshTokenAsync(string refresh);
        Task<UserDomainModel> GetUserByPhoneAsync(string phone);
        Task<UserDomainModel> GetUserByEmailAsync(string Email);
        Task<UserDomainModel> UpdateRefreshTokenAsync(UserDomainModel User);
        Task<PaginationEntity<UserDomainModel>> GetAllUsersAsync(int page, int limit);
    }

}