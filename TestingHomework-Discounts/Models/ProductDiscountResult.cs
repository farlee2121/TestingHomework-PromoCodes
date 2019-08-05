using System;

namespace TestingHomework_Discounts
{
    public class ProductDiscountResult
    {
        public Guid ProductId { get; set; }
        public double OriginalPrice { get; set; }
        public double FinalPrice { get; set; }
    }
}
