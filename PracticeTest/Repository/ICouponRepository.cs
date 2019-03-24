using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeTest.Models;

namespace PracticeTest.Repository
{
    public interface ICouponRepository
    {
        Task<IEnumerable<Coupon>> GetActiveCoupons();
    }
}
