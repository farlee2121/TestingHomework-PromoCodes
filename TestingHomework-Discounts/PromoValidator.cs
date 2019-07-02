using System;
using System.Collections.Generic;
using System.Linq;

namespace TestingHomework_Discounts
{
    public class PromoValidator
    {
        public Cart RedeemPromo(string code, Cart cart)
        {
            PromoRespository db = new PromoRespository();
            var dbPromo = db.PromoCodes.FirstOrDefault(_dbPromo => _dbPromo.Code == code);

            if (dbPromo != null)
            {
                DateTime now = DateTime.UtcNow;
                if(now < dbPromo.StartDate)
                {
                    cart.Errors.Add(new PromoError()
                    {
                        ErrorCode = PromoErrorType.NotStarted,
                        Message = $"Promo starts on {dbPromo.StartDate}"
                    });
                }
                if (dbPromo.EndDate < now) 
                {
                    cart.Errors.Add(new PromoError()
                    {
                        ErrorCode = PromoErrorType.Expired,
                        Message = $"Promo expired"
                    });
                }

                // apply promo
                cart.PromoCodes.Add(dbPromo);
            }
            else
            {
                cart.Errors.Add(new PromoError()
                {
                    ErrorCode = PromoErrorType.NoMatch,
                    Message = $"Promo {code} doesn't exist"
                });
            }

            return cart;
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

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        
    }

    public class Cart
    {
        public Id Id { get; set; }
        
        // this is a candidate for argument complexity, nest actual objects here
        public User User { get; set; }


        public IEnumerable<Product> Products { get; set; }

        public ICollection<PromoCode> PromoCodes { get; set; } = new List<PromoCode>();
        public ICollection<PromoError> Errors { get; set; } = new List<PromoError>();
    }

    public class User
    {
        public Id Id { get; set; }
    }

    public class Product
    {
        public Id Id { get; set; }
    }

    public class PromoError
    {
        public PromoErrorType ErrorCode { get; set; }

        public string Message { get; set; }

    }

    public enum PromoErrorType
    {
        Unknown = 0,
        NoMatch = 1,
        NotStarted = 2,
        Expired = 3,
        //NoRemainingUses = 3,
    }
}
