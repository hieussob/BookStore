using BookStore.Domain.Constants;
using BookStore.Domain.Entities;
using BookStore.Domain.IRepositories;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    public class CouponRepository : Repository<Coupon>, ICouponRepository
    {
        public CouponRepository(DBBookContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Coupon>> GetAvailableCouponsAsync()
        {
            var today = DateTime.Today;
            return await _dbSet
                .Where(x => x.Status.Equals(Constants.AVAILABLE) && x.ValidFrom <= today && x.ValidUntil >= today)
                .ToListAsync();
        }
    }
}
