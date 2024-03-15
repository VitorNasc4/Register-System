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
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public Task<User> GetUserByEmailAndPasswordAsyn(string email, string passwordHash)
        {
            return _dbContext.Users
                .SingleOrDefaultAsync(u => u.Email == email && u.Password == passwordHash);
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
    }
}