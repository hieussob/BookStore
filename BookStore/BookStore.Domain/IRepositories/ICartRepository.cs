using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;

namespace BookStore.Domain.IRepositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<object> AddToCart(Cart cart);

        Task<object> GetSanPhamTrongGioHang(Guid userId);
    }
}
