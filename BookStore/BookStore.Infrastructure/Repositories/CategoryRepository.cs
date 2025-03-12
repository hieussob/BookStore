using BookStore.Domain.Entities;
using BookStore.Domain.IRepositories;
using BookStore.Infrastructure.Data;

namespace BookStore.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DBBookContext context) : base(context)
        {
        }
    }
}
