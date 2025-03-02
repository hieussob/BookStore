using BookStore.Domain.Repositories;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookStore.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBBookContext _context;
        private IDbContextTransaction _transaction;
        public IBookRepository BookRepository {  get; set; }

        public UnitOfWork(DBBookContext context, IBookRepository bookRepository)
        {
            _context = context;
            BookRepository = bookRepository;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task RollBackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
