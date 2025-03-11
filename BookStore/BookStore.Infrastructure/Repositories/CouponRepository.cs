using BookStore.Domain.Entities;
using BookStore.Domain.IRepositories;
using BookStore.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .Where(x => x.Status == CouponStatus.khaDung && x.ValidFrom <= today && x.ValidUntil >= today)
                .ToListAsync();
        }
    }
}
