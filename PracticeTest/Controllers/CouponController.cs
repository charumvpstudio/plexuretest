using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using PracticeTest.Models;
using PracticeTest.Provider;
using PracticeTest.Repository;

namespace PracticeTest.Controllers
{
    public class CouponController : ApiController
    {
        private readonly Lazy<ICouponRepository> _couponRepository;
        private readonly Lazy<CouponManager> _couponManager;
        private IEnumerable<Func<Coupon, Guid, bool>> evaluators = new List<Func<Coupon, Guid, bool>>()
        {
            GetEvaluators
        };

        public CouponController(Lazy<ICouponRepository> couponRepository, Lazy<CouponManager> couponManager)
        {
            _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));
            _couponManager = couponManager ?? throw new ArgumentNullException(nameof(couponManager));
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
        [Route("api/coupon/canredeemcoupon/couponId/{userId}")]
        public async Task<bool> CanRedeemCouponAsync(Guid couponId, Guid userId)
        {
            return await _couponManager.Value.CanRedeemCoupon(couponId, userId, evaluators);
        }

        private static bool GetEvaluators(Coupon coupon, Guid userId)
        {
            return coupon.UserId.Equals(userId);
        }
    }
}
