using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using  Miccore.Net.webapi_template.User.Api.Repositories.User.DtoModels;
using  Miccore.Net.webapi_template.User.Api.Data;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;
using Microsoft.Extensions.DependencyInjection;
using Miccore.Net.webapi_template.User.Api.Entities;

namespace  Miccore.Net.webapi_template.User.Api.Repositories.User {

    public class UserRepository : IUserRepository {
        private readonly IApplicationDbContext _context;
        
        public UserRepository(
            IApplicationDbContext context
        ){
            _context = context;
        }

       public async Task<UserDtoModel> AuthenticateUser(UserDtoModel user)
        {
            var userGet = await _context.Users.SingleOrDefaultAsync(x => x.Phone == user.Phone  && x.DeletedAt != null);
            if (userGet == null || !BC.Verify(user.Password, userGet.Password))
            {
                return null;
            }

            return userGet;
        }

        public async Task<UserDtoModel> Create(UserDtoModel user)
        {
            user.Password = BC.HashPassword(user.Password);
            user.CreatedAt = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 01, 01, 0, 0, 0)).TotalSeconds;
            await _context.Users.AddAsync(user);
            await _context.SaveChanges();

           return user;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var dto = _context.Users.FirstOrDefault(x => x.Id == id);
            dto.DeletedAt = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 01, 01, 0, 0, 0)).TotalSeconds;
            await _context.SaveChanges();

            return id;
        }

        public async Task<PaginationEntity<UserDtoModel>> GetAllAsync(int page, int limit)
        {
            var users = await _context.Users
                                    .Include(x => x.Role)
                                    .Where(x => x.DeletedAt != null)
                                    .OrderBy(x => x.CreatedAt)
                                    .PaginateAsync(page, limit);
            
            return users;
        }

        public async Task<UserDtoModel> GetSingleAsync(int id)
        {
            var user =  await _context.Users
                                    .Include(x => x.Role)
                                    .FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt != null);
            return user;
        }

        public async Task<UserDtoModel> GetSingleByPhoneAsync(string phone)
        {
            var user =  await _context.Users
                                    .Include(x => x.Role)
                                    .FirstOrDefaultAsync(x => x.Phone == phone && x.DeletedAt != null);
            return user;
        }



        public async Task<UserDtoModel> GetSingleByEmailAsync(string Email)
        {
            var user =  await _context.Users
                                    .Include(x => x.Role)
                                    .FirstOrDefaultAsync(x => x.Email == Email && x.DeletedAt != null);
            return user;
        }

         public async Task<UserDtoModel> GetSingleByRefreshTokenAsync(string refresh)
        {
            var User =  await _context.Users
                                        .FirstOrDefaultAsync(x => x.RefreshToken == refresh && x.DeletedAt != null);
            return User;
        }

        public async Task<UserDtoModel> UpdateAsync(UserDtoModel user)
        {
            Contract.Requires(user != null);
            var dto = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id && x.DeletedAt != null);
            if (dto == null)
            {
                return new UserDtoModel();
            }
            dto.FirstName = user.FirstName;
            dto.LastName = user.LastName;
            dto.Email = user.Email;
            dto.Address = user.Address;
            dto.UpdatedAt = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 01, 01, 0, 0, 0)).TotalSeconds;
            await _context.SaveChanges();

            return dto;
        }

        public async Task<UserDtoModel> UpdateRefreshTokenAsync(UserDtoModel user)
        {
            Contract.Requires(user != null);
            var dto = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id && x.DeletedAt != null);
            if (dto == null)
            {
                return new UserDtoModel();
            }
            dto.RefreshToken = user.RefreshToken;
            dto.UpdatedAt = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 01, 01, 0, 0, 0)).TotalSeconds;
            await _context.SaveChanges();

            return dto;
        }


         public async Task<UserDtoModel> UpdatePasswordAsync(int id, string oldpassword, string newpassword)
        {
            var dto = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
             if (dto == null || !BC.Verify(oldpassword, dto.Password))
            {
                return new UserDtoModel();
            }

            dto.Password = BC.HashPassword(newpassword);
            dto.UpdatedAt = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 01, 01, 0, 0, 0)).TotalSeconds;
            await _context.SaveChanges();

            return dto;
        }
    }

}
