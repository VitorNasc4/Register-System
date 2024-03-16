using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjectName.Core.Entities;

namespace ProjectName.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<List<User>> GetAllUsersAsync(string query);
        Task<bool> UserExistAsync(string email);
        Task<User> GetUserByEmailAndPasswordAsyn(string email, string passwordHash);
        Task AddAsync(User user);
        Task SaveChangesAsync();
    }
}
