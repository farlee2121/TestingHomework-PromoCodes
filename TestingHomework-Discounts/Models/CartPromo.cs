using System;
using System.Collections.Generic;
using System.Text;

namespace TestingHomework_Discounts
{
    public class CartPromo
    {

        public Guid CartId { get; set; }
        public Guid PromoCodeId { get; set; }

        public Cart Cart { get; set; }
        public PromoCode PromoCode { get; set; }
    }
}
