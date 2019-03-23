using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticeTest.Models
{
    public class Coupon
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int MaximumCoupons { get; set; }
    }
}