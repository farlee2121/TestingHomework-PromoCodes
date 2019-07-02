using System.Collections.Generic;

namespace TestingHomework_Discounts
{
    public class Cart
    {
        public Id Id { get; set; }

        // this is a candidate for argument complexity, nest actual objects here
        public User User { get; set; }


        public IEnumerable<Product> Products { get; set; }

        public ICollection<PromoCode> PromoCodes { get; set; } = new List<PromoCode>();
        public ICollection<PromoError> PromoErrors { get; set; } = new List<PromoError>();
    }
}
