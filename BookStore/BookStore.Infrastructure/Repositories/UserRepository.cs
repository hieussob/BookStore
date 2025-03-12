using BookStore.Domain.Entities;
using BookStore.Domain.IRepositories;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DBBookContext context) : base(context)
        {
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbSet
            .Where(u => u.Email == email)
            .Select(u => new User
            {
                Id = u.Id,
                Email = u.Email,
                FullName = u.FullName
                //Roles = u.Roles.Select(r => r.Name).ToList()
            })
            .FirstOrDefaultAsync();
        }
    }
}
