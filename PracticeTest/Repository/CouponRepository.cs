using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PracticeTest.Models;

namespace PracticeTest.Repository
{
    /// <summary>
    /// Coupon Management Repository
    /// </summary>
    public class CouponRepository : ICouponRepository
    {
        List<Coupon> _coupons = new List<Coupon>();
        private readonly Lazy<IUserRepository> _userRepository;

        public CouponRepository(Lazy<IUserRepository> userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
           PopulateCoupons();
        }

        public async Task<IEnumerable<Coupon>> GetActiveCoupons()
        {
            int count = _coupons.Count(x => x.IsActive);
            return (count >= 100 && count <= 500) ? _coupons.Where(x=>x.EndDate.Date > DateTime.Now.Date).ToList() : null;
        }

        //public async Task<bool> CanRedeemCoupon(int userId, int couponId)
        //{
        //    int couponId = _userRepository.Value.GetUsers().FirstOrDefault();
        //    return false;
        //}

        private void PopulateCoupons()
        {
            _coupons.Add(new Coupon() { Id = Guid.NewGuid(), Title = "A", StartDate = new DateTime(2019,3,1), EndDate = new DateTime(2019, 4, 1), MaximumCouponsPerUser = 10, MaximumCouponsAllUsers = 100, IsActive = true});
            _coupons.Add(new Coupon() { Id = Guid.NewGuid(), Title = "B", StartDate = new DateTime(2019, 3, 2), EndDate = new DateTime(2019, 4, 1), MaximumCouponsPerUser = 7, MaximumCouponsAllUsers = 100, IsActive = false});
        }

        public Task<bool> CanRedeemCoupon(int userId)
        {
            throw new NotImplementedException();
        }
    }
}