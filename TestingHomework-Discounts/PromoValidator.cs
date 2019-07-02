using Microsoft.EntityFrameworkCore;
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
        public CartDiscountResult CalculateDiscounts(Cart cart)
        {
            double originalPrice = cart.Products.Sum(_product => _product.Price);

            double calculatedPrice = originalPrice;
            List<ProductDiscountResult> productDiscounts = new List<ProductDiscountResult>();
            foreach (var promo in cart.PromoCodes)
            {
                if(promo.Product != null)
                {
                    var discountedPrice = EnforceZeroMinimum(promo.Product.Price - promo.DollarDiscount);
                    productDiscounts.Add(new ProductDiscountResult()
                    {
                        ProductId = promo.Product.Id,
                        FinalPrice = discountedPrice,
                        OriginalPrice = promo.Product.Price
                    });
                    calculatedPrice -= discountedPrice;
                }
                else
                {
                    calculatedPrice -= promo.DollarDiscount;
                }
            }

            double finalPrice = EnforceZeroMinimum(calculatedPrice);

            return new CartDiscountResult()
            {
                OriginalPrice = originalPrice,
                FinalPrice = finalPrice,
                ProductDiscounts = productDiscounts,
            };
        }

        private double EnforceZeroMinimum(double price)
        {
            if(price >= 0)
            {
                return price;
            }
            else
            {
                return 0;
            }
        }
    }
}
