using System;
using System.Collections.Generic;

namespace TestingHomework_Discounts
{
    public class Cart
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public ICollection<PromoCode> PromoCodes { get; set; } = new List<PromoCode>();
        public ICollection<PromoError> PromoErrors { get; set; } = new List<PromoError>();
    }
}
