using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(DBBookContext context) : base(context)
        {
        }

        public async Task<bool> DeleteFavoriteBooksAsync(Guid bookId, Guid userID)
        {
            //var favoriteBooks = await _context.Wishlists.FirstOrDefaultAsync(x => x.BookId == bookId && x.UserId == userID);
            var favoriteBooks = await _context.Wishlists.FindAsync(bookId, userID);
            if (favoriteBooks == null)
            {
                return false;
            }

            _context.Wishlists.Remove(favoriteBooks);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<object> GetBookBySearchAsync(string keyword, int page)
        {
            var query = (from bk in _dbSet
                         join ba in _context.BookAuthors on bk.Id equals ba.BookId into baGroup
                         from ba in baGroup.DefaultIfEmpty()
                         join at in _context.Authors on ba.AuthorId equals at.Id into atGroup
                         from at in atGroup.DefaultIfEmpty()
                         group new { bk, at.FullName } by new
                         {
                             bk.Id,
                             bk.Title,
                             bk.Price,
                             bk.PriceDiscount,
                             bk.ImageUrls,
                         } into grouped
                         select new
                         {
                             grouped.Key.Id,
                             grouped.Key.Title,
                             grouped.Key.PriceDiscount,
                             grouped.Key.ImageUrls,
                             grouped.Key.Price,
                             AuthorName = string.Join(", ", grouped.Select(x => x.FullName).ToArray()),
                         });
            var totalRecords = await query.CountAsync(x => x.AuthorName.ToLower().Contains(keyword.ToLower()) || x.Title.ToLower().Contains(keyword.ToLower()));
            var book = query.Where(x => x.AuthorName.ToLower().Contains(keyword.ToLower()) || x.Title.ToLower().Contains(keyword.ToLower())).Skip(page * 20).Take(20);

            return new { TotalRecords = totalRecords, Books = book };
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId)
        {
            return await _dbSet.Where(x => x.BookAuthors.Any(y => y.AuthorId == authorId)).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(Guid id, int count)
        {
            return await _dbSet.Where(x => x.CategoryId == id).Take(count).ToListAsync();
        }

        public async Task<IEnumerable<Object>> GetBooksByMonthAsync(int count)
        {
            var startOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1);

            var books = await (from book in _context.Books
                               join orderDetail in _context.OrderDetails on book.Id equals orderDetail.BookId into orderDetails
                               from orderDetail in orderDetails.DefaultIfEmpty()
                               where orderDetail.Created >= startOfMonth && orderDetail.Created < endOfMonth || orderDetail == null// thêm trạng thái là đã giao hàng
                               group orderDetail by new { book.Id, book.Title, book.Price, book.Code, book.PriceDiscount, book.ImageUrls } into bookGroup
                               select new
                               {
                                   bookGroup.Key.Id,
                                   bookGroup.Key.Title,
                                   bookGroup.Key.Price,
                                   bookGroup.Key.Code,
                                   bookGroup.Key.PriceDiscount,
                                   bookGroup.Key.ImageUrls,
                                   numberSell = bookGroup.Sum(x => x.Quantity)
                               })
                       .OrderByDescending(b => b.numberSell)
                       .Take(count)
                       .ToListAsync();

            return books;

        }

        public async Task<IEnumerable<object>> GetBooksByWeekAsync(int count)
        {
            var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7);

            var books = await (from book in _context.Books
                               join orderDetail in _context.OrderDetails on book.Id equals orderDetail.BookId into orderDetails
                               from orderDetail in orderDetails.DefaultIfEmpty()
                               where orderDetail.Created >= startOfWeek && orderDetail.Created < endOfWeek || orderDetail == null // thêm trạng thái là đã giao hàng
                               group orderDetail by new { book.Id, book.Title, book.Price, book.Code, book.PriceDiscount, book.ImageUrls } into bookGroup
                               select new
                               {
                                   bookGroup.Key.Id,
                                   bookGroup.Key.Title,
                                   bookGroup.Key.Price,
                                   bookGroup.Key.Code,
                                   bookGroup.Key.PriceDiscount,
                                   bookGroup.Key.ImageUrls,
                                   numberSell = bookGroup.Sum(x => x.Quantity)
                               })
                       .OrderByDescending(b => b.numberSell)
                       .Take(count)
                       .ToListAsync();

            return books;

        }

        public async Task<object> GetBooksFilterAsync(string keyword, Guid categoryId, int rangeFrom, int rangeTo, int order, int page)
        {
            var totalRecords = await (from bk in _context.Books
                                      where ((rangeFrom == 0 && rangeTo == 0)
                                              || (bk.PriceDiscount != null ? (bk.PriceDiscount >= rangeFrom && bk.PriceDiscount <= rangeTo) : (bk.Price >= rangeFrom && bk.Price <= rangeTo)))
                                              && (categoryId == Guid.Empty || bk.CategoryId == categoryId)
                                              && (keyword == "null" || bk.Title == null || bk.Title.Contains(keyword))
                                      select bk).CountAsync();

            var book = await (from bk in _context.Books
                              where ((rangeFrom == 0 && rangeTo == 0)
                                    || (bk.PriceDiscount != null ? (bk.PriceDiscount >= rangeFrom && bk.PriceDiscount <= rangeTo) : (bk.Price >= rangeFrom && bk.Price <= rangeTo)))
                                    && (categoryId == Guid.Empty || bk.CategoryId == categoryId)
                                    && (keyword == "null" || bk.Title == null || bk.Title.Contains(keyword))
                              orderby
                                    (order == 0 ? bk.Created : null) descending,
                                    (order == 1 ? (bk.PriceDiscount ?? bk.Price) : null) ascending,
                                    (order == 2 ? (bk.PriceDiscount ?? bk.Price) : null) descending,
                                    (order == 3 ? bk.Title : null) ascending
                              select bk).Skip(page * 20).Take(20).ToListAsync();

            return new { TotalRecords = totalRecords, Books = book };

        }

        public async Task<IEnumerable<Book>> GetBooksUnderPriceAsync(int price)
        {
            return await _dbSet.Where(x => x.Price <= price).ToListAsync();
        }

        public Task<IEnumerable<Book>> GetCountBook(Guid bookId, int quantity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Book>> GetFavoriteBooksAsync(Guid userId)
        {
            var bookIDs = await _context.Wishlists.Where(x => x.UserId == userId).Select(x => x.BookId).ToListAsync();

            return await _dbSet.Where(x => bookIDs.Contains(x.Id)).ToListAsync();

        }

        public async Task<bool> GetFavoritePostsAsync(Wishlist wishlist)
        {
            _context.Wishlists.Add(wishlist);
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<IEnumerable<Book>> GetHighestReviewBooksAsync(int count)
        {
            var books = await (from book in _dbSet
                               join review in _context.Reviews on book.Id equals review.BookId into reviewsGroup
                               from review in reviewsGroup.DefaultIfEmpty()
                               group review by new { book.Id, book.Title, book.Price, book.Code, book.PriceDiscount, book.ImageUrls } into bookGroup
                               select new
                               {
                                   bookGroup.Key.Id,
                                   bookGroup.Key.Title,
                                   bookGroup.Key.Price,
                                   bookGroup.Key.Code,
                                   bookGroup.Key.PriceDiscount,
                                   bookGroup.Key.ImageUrls,
                                   AverageStar = bookGroup.Average(r => r.Stars)
                               })
                              .OrderByDescending(b => b.AverageStar)
                              .Take(count)
                              .ToListAsync();

            return books.Select(b => new Book
            {
                Id = b.Id,
                Title = b.Title,
                Price = b.Price,
                Code = b.Code,
                PriceDiscount = b.PriceDiscount,
                ImageUrls = b.ImageUrls
            });
        }

        public async Task<IEnumerable<Book>> GetNewestBooksAsync(int count)
        {
            return await _dbSet.OrderByDescending(x => x.ImportDate).Take(count).ToListAsync();
        }

    }
}
