using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;

namespace BookStore.Domain.IRepositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
    }
}
