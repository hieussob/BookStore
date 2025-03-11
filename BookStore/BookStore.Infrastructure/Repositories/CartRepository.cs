using BookStore.Domain.Entities;
using BookStore.Domain.IRepositories;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BookStore.Infrastructure.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(DBBookContext context) : base(context)
        {
        }

        public async Task<object> AddToCart(Cart cart)
        {
            var findUser = await _dbSet.FirstOrDefaultAsync(x => x.UserId == cart.UserId);
            if (findUser == null)
            {
                var items = new List<Item>();
                if (!string.IsNullOrEmpty(cart.IdsItem))
                {
                    var newItem = JsonConvert.DeserializeObject<Item>(cart.IdsItem);
                    items.Add(newItem);
                }
                cart.IdsItem = JsonConvert.SerializeObject(items);
                _dbSet.Add(cart);
            }
            else
            {
                var idsItem = !string.IsNullOrEmpty(findUser.IdsItem)
                    ? JsonConvert.DeserializeObject<List<Item>>(findUser.IdsItem)
                    : new List<Item>();

                if (cart.IdsItem != null)
                {
                    var newItem = JsonConvert.DeserializeObject<Item>(cart.IdsItem);
                    var existingItem = idsItem.FirstOrDefault(item => item.BookId == newItem.BookId);
                    if (existingItem != null)
                    {
                        existingItem.Quantity = newItem.Quantity;
                    }
                    else
                    {
                        idsItem.Add(newItem);
                    }
                }

                findUser.IdsItem = JsonConvert.SerializeObject(idsItem);
                _dbSet.Update(findUser);
            }

            await _context.SaveChangesAsync();
            return new { Status = "success" };

        }

        public async Task<object> GetSanPhamTrongGioHang(Guid userId)
        {
            var strItems = await _dbSet.Where(x => x.UserId == userId).Select(x => x.IdsItem).FirstOrDefaultAsync();
            if (strItems == null) return null;

            var listItem = JsonConvert.DeserializeObject<List<Item>>(strItems);
            if (listItem == null) return null;

            var idsItem = listItem.Select(item => item.BookId).ToArray();
            var books = await _context.Books.Where(bk => idsItem.Contains(bk.Id)).ToListAsync();

            var result = books.Select(book => new
            {
                Book = book,
                SoLuong = listItem.FirstOrDefault(item => item.BookId == book.Id)?.Soluong ?? 0
            }).ToList();

            foreach (var item in books)
            {
                item.ImageUrls = item.ImageUrls.Split(",")[0];
            }

            return result;
        }

        public class Item
        {
            public Guid BookId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
