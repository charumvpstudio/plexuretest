using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PracticeTest.Repository;

namespace PracticeTest.Controllers
{
    public class CouponController : ApiController
    {
        private readonly Lazy<ICouponRepository> _couponRepository;

        public CouponController(Lazy<ICouponRepository> couponRepository)
        {
            if ((_couponRepository = couponRepository) == null)
                throw new ArgumentNullException(nameof(couponRepository));
        }


    }
}
