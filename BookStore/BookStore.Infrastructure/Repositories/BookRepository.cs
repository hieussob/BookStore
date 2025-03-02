using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using BookStore.Infrastructure.Data;

namespace BookStore.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(DBBookContext context) : base(context)
        {
        }
    }
}
