using System;
using System.Collections.Generic;
using System.Linq;

namespace TestingHomework_Discounts
{
    public class PromoValidator
    {
        public void RedeemPromo(string code, Cart cart)
        {
            PromoRespository db = new PromoRespository();
            var dbPromo = db.PromoCodes.FirstOrDefault(_dbPromo => _dbPromo.Code == code);

            if (dbPromo != null)
            {

            }
            else
            {

            }
        }
    }


    public class PromoRespository
    {
        public List<PromoCode> PromoCodes { get; set; }
    }

    public class PromoCode
    {
        public Id Id { get; set; }
        public string Code { get; set; }

        public double Discount { get; set; }
        public int DiscountType { get; set; }
    }

    public class Cart
    {
        public Id Id { get; set; }
        
        // this is a candidate for argument complexity, nest actual objects here
        public User User { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }

    public class User
    {
        public Id Id { get; set; }
    }

    public class Product
    {
        public Id Id { get; set; }
    }
}
