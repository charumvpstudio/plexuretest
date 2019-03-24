using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticeTest.Logging;
using PracticeTest.Models;
using PracticeTest.Provider;
using Xunit;
using Logger = PracticeTest.Logging.Logger;
using Rhino.Mocks;

namespace PlexureTests
{
    public class CouponManagerTests
    {
        public IEnumerable<Func<Coupon, Guid, bool>> evaluators = new List<Func<Coupon, Guid, bool>>()
        {
            GetEvaluators
        };

        public static IEnumerable<object[]> ConstructorArgs
        {
            get
            {
                yield return new object[] { new Logger(), new CouponProvider(), false };
                yield return new object[] { new Logger(), null, true };
                yield return new object[] { null, new CouponProvider(), true };
                yield return new object[] { null, null, true };
            }
        }

        [Theory]
        [MemberData("ConstructorArgs")]
        public void Constructor_CorrectBehaviour(ILogger logger, ICouponProvider couponProvider, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<ArgumentNullException>(() => new CouponManager(logger, couponProvider));
            }
            else
            {
                Assert.NotNull(new CouponManager(logger, couponProvider));
            }
        }

        [Theory]
        [InlineData("3e595496-1549-410d-a579-9f6d09b2602a", "4743d0e4-c303-4679-8533-9f5f312397a4", null)]
        public async Task CanRedeemCoupon_Null_Evaluators(Guid couponId, Guid userId, IEnumerable<Func<Coupon, Guid, bool>> evaluators)
        {
            var couponManager = this.CreateCouponManager();
            await Assert.ThrowsAsync<ArgumentNullException>(() => couponManager.CanRedeemCoupon(couponId, userId, evaluators));
        }

        [Theory]
        [InlineData("3e595496-1549-410d-a579-9f6d09b2602a", "4743d0e4-c303-4679-8533-9f5f312397a4", true)]
        public async Task CanRedeemCoupon_Null_Retrieve_Coupon(Guid couponId, Guid userId, bool value)
        {
            var couponManager = this.CreateCouponManager(value);
           await Assert.ThrowsAsync<KeyNotFoundException>(() => couponManager.CanRedeemCoupon(couponId, userId, evaluators));
        }

        [Theory]
        [InlineData("3e595496-1549-410d-a579-9f6d09b2602a", "4743d0e4-c303-4679-8533-9f5f312397a4")]
        public async Task CanRedeemCoupon_ReturnsTrue(Guid couponId, Guid userId)
        {
            var couponManager = this.CreateCouponManager();
            Assert.True(couponManager.CanRedeemCoupon(couponId, userId, evaluators).Result);
        }

        private CouponManager CreateCouponManager(bool value = false)
        {
            var mockLogger = MockRepository.GenerateStub<ILogger>();
            var mockCouponProvider = MockRepository.GenerateStub<ICouponProvider>();
            Coupon coupon = null;
            if (value)
               mockCouponProvider.Stub(i => i.Retrieve(Arg<Guid>.Is.Anything)).Return(Task.FromResult(coupon));
            else
                mockCouponProvider.Stub(i => i.Retrieve(Arg<Guid>.Is.Anything)).
                    Return(Task.FromResult(new Coupon(){ Id = new Guid("3e595496-1549-410d-a579-9f6d09b2602a"), Title = "A", StartDate = new DateTime(2019, 3, 1), EndDate = new DateTime(2019, 4, 1), MaximumCouponsPerUser = 10, MaximumCouponsAllUsers = 100, IsActive = true, UserId = new Guid("4743d0e4-c303-4679-8533-9f5f312397a4") }));

            return new CouponManager(mockLogger, mockCouponProvider);
        }

        private static bool GetEvaluators(Coupon coupon, Guid userId)
        {
            return coupon.UserId.Equals(userId);
        }
    }
}
