using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticeTest.Models
{
    public class Coupon
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int MaximumCouponsPerUser { get; set; }

        public int MaximumCouponsAllUsers { get; set; }

        public bool IsActive { get; set; }

    }
}