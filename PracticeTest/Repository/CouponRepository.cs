using System;
using System.Collections.Generic;
using System.Linq;
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

        public CouponRepository()
        {
            PopulateCoupons();
        }

        public IEnumerable<Coupon> GetActiveCoupons()
        {
            return _coupons.Where(x=>x.EndDate.Date < DateTime.Now.Date).ToList();
        }

        private void PopulateCoupons()
        {
            _coupons.Add(new Coupon() { Id = 1, Title = "A", StartDate = new DateTime(2019,3,1), EndDate = new DateTime(2019, 4, 1), MaximumCouponsPerUser = 10, MaximumCouponsAllUsers = 100 });
            _coupons.Add(new Coupon() { Id = 2, Title = "B", StartDate = new DateTime(2019, 3, 2), EndDate = new DateTime(2019, 4, 1), MaximumCouponsPerUser = 7, MaximumCouponsAllUsers = 100 });
        }
    }
}