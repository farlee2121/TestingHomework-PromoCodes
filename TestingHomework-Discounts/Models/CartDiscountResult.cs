using System.Collections.Generic;

namespace TestingHomework_Discounts
{
    public class CartDiscountResult
    {
        public double OriginalPrice { get; set; }
        public double FinalPrice { get; set; }
        public IEnumerable<ProductDiscountResult> ProductDiscounts { get; set; }

        
    }
}
