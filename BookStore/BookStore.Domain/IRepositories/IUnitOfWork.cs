namespace BookStore.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository BookRepository { get; }

        Task<int> SaveChangeAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollBackTransactionAsync();
    }
}
