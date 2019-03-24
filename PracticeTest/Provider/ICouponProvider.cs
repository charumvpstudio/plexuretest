using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeTest.Models;

namespace PracticeTest.Provider
{
    public interface ICouponProvider
    {
        Task<Coupon> Retrieve(Guid couponId);
    }
}
