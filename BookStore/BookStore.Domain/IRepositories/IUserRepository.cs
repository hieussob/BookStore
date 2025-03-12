using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;

namespace BookStore.Domain.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string email);
    }
}
