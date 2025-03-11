using BookStore.Domain.Entities;

namespace BookStore.Domain.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksUnderPriceAsync(int price);
        Task<IEnumerable<Book>> GetNewestBooksAsync(int count);
        Task<IEnumerable<Book>> GetHighestReviewBooksAsync(int count);
        Task<IEnumerable<object>> GetBooksByWeekAsync(int count);
        Task<IEnumerable<object>> GetBooksByMonthAsync(int count);
        Task<object> GetBooksFilterAsync(string keyword, Guid categoryId, int rangeFrom, int rangeTo, int order, int page);
        Task<IEnumerable<Book>> GetBooksByCategoryAsync(Guid id, int count);
        Task<IEnumerable<Book>> GetFavoriteBooksAsync(Guid authorId);
        Task<bool> DeleteFavoriteBooksAsync(Guid bookId, Guid authorId);
        Task<bool> GetFavoritePostsAsync(Wishlist wishlist);
        Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId);
        Task<object> GetBookBySearchAsync(string keyword, int page);
        Task<IEnumerable<Book>> GetCountBook(Guid bookId, int quantity);
    }
}
