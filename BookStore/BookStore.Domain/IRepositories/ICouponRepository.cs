using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;

namespace BookStore.Domain.IRepositories
{
    public interface ICouponRepository : IRepository<Coupon>
    {
        Task<IEnumerable<Coupon>> GetAvailableCouponsAsync();
    }
}
