using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeTest.Models;

namespace PracticeTest.Provider
{
    public class CouponProvider : ICouponProvider
    {
        public Task<Coupon> Retrieve(Guid couponId)
        {
            throw new NotImplementedException();
        }
    }
}
