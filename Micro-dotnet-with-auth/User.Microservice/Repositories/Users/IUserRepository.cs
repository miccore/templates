using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.Microservice.Repositories.User.DtoModels;

namespace User.Microservice.Repositories.User {

    public interface IUserRepository{

        Task<IEnumerable<UserDtoModel>> GetAllAsync();
        Task<UserDtoModel> GetSingleAsync(int id);
        Task<UserDtoModel> AuthenticateUser(UserDtoModel user);
        Task<UserDtoModel> Create(UserDtoModel user);
        Task<UserDtoModel> UpdateAsync(UserDtoModel user);
        Task<UserDtoModel> UpdatePasswordAsync(int id, string oldpassword, string newpassword);
        Task<UserDtoModel> GetSingleByRefreshTokenAsync(string refresh);
        Task<int> DeleteAsync(int id);
        Task<UserDtoModel> GetSingleByPhoneAsync(string phone);
        Task<UserDtoModel> GetSingleByEmailAsync(string Email);

    }

}