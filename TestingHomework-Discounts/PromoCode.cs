using System;

namespace TestingHomework_Discounts
{
    public class PromoCode
    {
        public Guid Id { get; set; }
        public string Code { get; set; }

        public double DollarDiscount { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int MaxRedemptionCount { get; set; }
        public int RedemptionCount { get; set; }

        public Product Product { get; set; }
    }
}
