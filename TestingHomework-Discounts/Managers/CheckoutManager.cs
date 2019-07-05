using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestingHomework_Discounts.Managers
{
    public class CheckoutManager
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

        public Cart GetUserCart(Guid userId)
        {
            using (PromoRepository db = new PromoRepository())
            {
                Cart userCart = db.Carts
                    .Include(_cart => _cart.User)
                    .FirstOrDefault(_cart => _cart.User.Id == userId);

                userCart.Products = db.CartProducts.Include(cp => cp.Product).Where(cp => cp.CartId == userCart.Id).Select(cp => cp.Product).ToList();
                userCart.PromoCodes = db.CartPromos.Include(cp => cp.PromoCode).Where(cp => cp.CartId == userCart.Id).Select(cp => cp.PromoCode).ToList();

                return userCart;
            }
        }

        public Cart SaveCart(Cart cart)
        {
            using (PromoRepository db = new PromoRepository())
            {
                var productCache = cart.Products.ToList();
                var promoCache = cart.PromoCodes.ToList();
                cart.Products = new List<Product>();
                cart.PromoCodes = new List<PromoCode>();

                if (cart.User.Id == Guid.Empty)
                {
                    db.Users.Add(cart.User);
                }
                else
                {
                    db.Users.Attach(cart.User);
                    db.Entry(cart.User).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }


                if (cart.Id == Guid.Empty)
                {
                    db.Carts.Add(cart);
                }
                else
                {
                    db.Carts.Attach(cart);
                    db.Entry(cart).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                db.SaveChanges();

                var existingProductRelations = db.CartProducts.Where(cp => cp.CartId == cart.Id);
                var existingProductIds = existingProductRelations.Select(cp => cp.ProductId);
                var requestedProductIds = productCache.Select(product => product.Id);

                var toAddProductIds = requestedProductIds.Except(existingProductIds);
                var toRemoveProductIds = existingProductIds.Except(requestedProductIds);

                var toAddCartProductModels = toAddProductIds.Select(productId => new CartProduct() { CartId = cart.Id, ProductId = productId });
                db.CartProducts.AddRange(toAddCartProductModels);
                
                foreach (var toRemoveId in toRemoveProductIds)
                {
                    db.CartProducts.Remove(db.CartProducts.First(cp => cp.CartId == cart.Id && cp.ProductId == toRemoveId));
                }

                var existingPromoRelations = db.CartPromos.Where(cp => cp.CartId == cart.Id);
                var existingPromoIds = existingPromoRelations.Select(cp => cp.PromoCodeId);
                var requestedPromoIds = promoCache.Select(product => product.Id);

                var toAddPromoIds = requestedPromoIds.Except(existingPromoIds);
                var toRemovePromoIds = existingPromoIds.Except(requestedPromoIds);

                var toAddCartPromoModels = toAddPromoIds.Select(promoId => new CartPromo() { CartId = cart.Id, PromoCodeId = promoId });
                db.CartPromos.AddRange(toAddCartPromoModels);

                foreach (var toRemoveId in toRemoveProductIds)
                {
                    db.CartPromos.Remove(db.CartPromos.First(cp => cp.CartId == cart.Id && cp.PromoCodeId == toRemoveId));
                }

                db.SaveChanges();
                cart.Products = productCache;
                cart.PromoCodes = promoCache;
            }

            return cart;
        }
    }
}
