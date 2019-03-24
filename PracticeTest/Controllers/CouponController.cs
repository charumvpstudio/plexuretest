using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using PracticeTest.Models;
using PracticeTest.Repository;

namespace PracticeTest.Controllers
{
    public class CouponController : ApiController
    {
        private readonly Lazy<ICouponRepository> _couponRepository;

        public CouponController(Lazy<ICouponRepository> couponRepository)
        {
            _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));
        }

        /// <summary>
        /// Get all active coupons
        /// </summary>
        /// <returns>Get all active coupons</returns>
        [HttpGet]
        [Route("api/coupon/activecoupons")]
        public async Task<List<Coupon>> GetActiveCouponsAsync()
        {
            return (await _couponRepository.Value.GetActiveCoupons()).ToList();
        }

        /// <summary>
        /// To determine customer can redeem a coupon
        /// </summary>
        /// <returns>returns whether customer coupon can be redeemed or not</returns>
        [HttpGet]
        [Route("api/coupon/canredeemcoupon/{userId}")]
        public async Task<bool> CanRedeemCouponAsync(int userId)
        {
            return await _couponRepository.Value.CanRedeemCoupon(userId);
        }
    }
}
