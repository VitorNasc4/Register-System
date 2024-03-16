using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectName.Core.Entities;
using ProjectName.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ProjectName.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ProjectNameDbContext _dbContext;

        public UserRepository(ProjectNameDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<User> GetUserByEmailAndPasswordAsyn(string email, string passwordHash)
        {
            return await _dbContext.Users
                .SingleOrDefaultAsync(u => u.Email == email && u.Password == passwordHash);
        }

        public bool UserExistAsync(string email)
        {
            return _dbContext.Users
                .Any(u => u.Email == email);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _dbContext.Users
                .SingleOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsersAsync(string query)
        {
            return await _dbContext.Users.ToListAsync();
        }
    }
}