using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.Microservice.Services.User.DomainModels;

namespace User.Microservice.Services.User {

    public interface IUserService{


        Task<IEnumerable<UserDomainModel>> GetAllUsersAsync();
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
    }

}