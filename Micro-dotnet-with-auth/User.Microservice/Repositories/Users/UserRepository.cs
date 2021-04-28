using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using User.Microservice.Repositories.User.DtoModels;
using User.Microservice.Data;
using Microsoft.EntityFrameworkCore;

namespace User.Microservice.Repositories.User {

    public class UserRepository : IUserRepository {
        private readonly IApplicationDbContext _context;
        
        public UserRepository(
            IApplicationDbContext context
        ){
            _context = context;
        }

        public async Task<UserDtoModel> AuthenticateUser(UserDtoModel user)
        {
            var userGet = await _context.Users.SingleOrDefaultAsync(x => x.Phone == user.Phone && x.Password == user.Password);
            return userGet;
        }

        public async Task<UserDtoModel> Create(UserDtoModel user)
        {
            user.Created_at = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 01, 01, 0, 0, 0)).TotalSeconds;
            await _context.Users.AddAsync(user);
            await _context.SaveChanges();

           return user;
        }

        public async Task<int> DeleteAsync(int id)
        {
           var dto = _context.Users.FirstOrDefault(x => x.Id == id);
            _context.Users.Remove(dto);
            await _context.SaveChanges();

            return id;
        }

        public async Task<IEnumerable<UserDtoModel>> GetAllAsync()
        {
            var users = await _context.Users
                                    .Include(x => x.Role)
                                    .ToListAsync();
            
            return users;
        }

        public async Task<UserDtoModel> GetSingleAsync(int id)
        {
            var user =  await _context.Users
                                    .Include(x => x.Role)
                                    .FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<UserDtoModel> GetSingleByPhoneAsync(string phone)
        {
            var user =  await _context.Users
                                    .Include(x => x.Role)
                                    .FirstOrDefaultAsync(x => x.Phone == phone);
            return user;
        }



        public async Task<UserDtoModel> GetSingleByEmailAsync(string Email)
        {
            var user =  await _context.Users
                                    .Include(x => x.Role)
                                    .FirstOrDefaultAsync(x => x.Email == Email);
            return user;
        }

         public async Task<UserDtoModel> GetSingleByRefreshTokenAsync(string refresh)
        {
            var User =  await _context.Users
                                        .FirstOrDefaultAsync(x => x.RefreshToken == refresh);
            return User;
        }

        public async Task<UserDtoModel> UpdateAsync(UserDtoModel user)
        {
            Contract.Requires(user != null);
            var dto = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            dto.FirstName = user.FirstName;
            dto.LastName = user.LastName;
            dto.Email = user.Email;
            dto.Address = user.Address;
            await _context.SaveChanges();

            return dto;
        }

         public async Task<UserDtoModel> UpdatePasswordAsync(int id, string oldpassword, string newpassword)
        {
            var dto = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if(dto.Password == oldpassword){
                dto.Password = newpassword;
                await _context.SaveChanges();

                return dto;
            }else{
                return null;
            }
        }
    }

}
