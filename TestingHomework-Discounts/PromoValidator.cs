﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestingHomework_Discounts
{
    public class PromoValidator
    {
        public Cart RedeemPromo(string code, Cart cart)
        {
            using (PromoRepository db = new PromoRepository())
            {

                var dbPromo = db.PromoCodes.Include(_promo => _promo.Product).FirstOrDefault(_dbPromo => _dbPromo.Code == code);

                if (dbPromo != null)
                {
                    DateTime now = DateTime.UtcNow;
                    if (now < dbPromo.StartDate)
                    {
                        cart.PromoErrors.Add(new PromoError()
                        {
                            ErrorCode = PromoErrorType.NotStarted,
                            Message = $"Promo starts on {dbPromo.StartDate}"
                        });
                    }
                    if (dbPromo.EndDate < now)
                    {
                        cart.PromoErrors.Add(new PromoError()
                        {
                            ErrorCode = PromoErrorType.Expired,
                            Message = $"Promo expired"
                        });
                    }
                    if (dbPromo.RedemptionCount >= dbPromo.MaxRedemptionCount)
                    {
                        cart.PromoErrors.Add(new PromoError()
                        {
                            ErrorCode = PromoErrorType.MaxRedemptions,
                            Message = $"Promo expired"
                        });
                    }

                    if (cart.Products.Select(_product => _product.Id).Contains(dbPromo.Id) == false)
                    {
                        cart.PromoErrors.Add(new PromoError()
                        {
                            ErrorCode = PromoErrorType.NoRelatedProduct,
                            Message = "Promo does not apply to any items in the cart"
                        });
                    }

                    dbPromo.RedemptionCount += 1;
                    db.SaveChanges();

                    // apply promo
                    cart.PromoCodes.Add(dbPromo);
                }
                else
                {
                    cart.PromoErrors.Add(new PromoError()
                    {
                        ErrorCode = PromoErrorType.InvalidPromo,
                        Message = $"Promo {code} doesn't exist"
                    });
                }

                return cart;
            }

        }

        //TODO: cart price calculator
    }

    public class PromoCode
    {
        public Id Id { get; set; }
        public string Code { get; set; }

        public double Discount { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int MaxRedemptionCount { get; set; }
        public int RedemptionCount { get; set; }

        public Product Product { get; set; }
    }

    public class Cart
    {
        public Id Id { get; set; }

        // this is a candidate for argument complexity, nest actual objects here
        public User User { get; set; }


        public IEnumerable<Product> Products { get; set; }

        public ICollection<PromoCode> PromoCodes { get; set; } = new List<PromoCode>();
        public ICollection<PromoError> PromoErrors { get; set; } = new List<PromoError>();
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
        InvalidPromo = 1,
        NotStarted = 2,
        Expired = 3,
        MaxRedemptions = 4,
        NoRelatedProduct = 5,
    }
}