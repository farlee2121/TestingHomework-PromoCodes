using Microsoft.EntityFrameworkCore;
using Shared.DatabaseContext;
using Shared.DatabaseContext.DBOs;
using Shared.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Accessors.DatabaseAccessors
{
    public interface ICheckoutAccessor
    {
        SaveResult<Cart> RedeemPromo(string code, Cart cart);
        SaveResult<CartDiscountResult> CalculateDiscounts(Cart cart);
        SaveResult<Cart> GetUserCart(Guid userId);
        SaveResult<Cart> SaveCart(Cart cart);


    }
    public class CheckoutAccessor : ICheckoutAccessor
    {
        Cart_Mapper mapper = new Cart_Mapper();
        PromoCode_Mapper promomapper = new PromoCode_Mapper();
        CartDiscountResult_Mapper cartDiscountResultMapper  = new CartDiscountResult_Mapper();

        public SaveResult<Cart> RedeemPromo(string code, Cart cart)
        {
            SaveResult <Cart> cartResult;
            CartDTO dbCartModel = mapper.ContractToModel(cart);
            using (TestingHomeworkContext db = new TestingHomeworkContext())
            {
                PromoCodeDTO dbPromo = db.PromoCodes.Include(_promo => _promo.Product).FirstOrDefault(_dbPromo => _dbPromo.Code == code);
             
                if (dbPromo != null)
                {
                    DateTime now = DateTime.UtcNow;
                    if (now < dbPromo.StartDate)
                    {
                        dbCartModel.PromoErrors.Add(new PromoErrorDTO()
                        {
                            ErrorCode = PromoErrorTypeDTO.NotStarted,
                            Message = $"Promo starts on {dbPromo.StartDate}"
                        });
                    }
                    if (dbPromo.EndDate < now)
                    {
                        dbCartModel.PromoErrors.Add(new PromoErrorDTO()
                        {
                            ErrorCode = PromoErrorTypeDTO.Expired,
                            Message = $"Promo expired"
                        });
                    }
                    if (dbPromo.RedemptionCount >= dbPromo.MaxRedemptionCount)
                    {
                        dbCartModel.PromoErrors.Add(new PromoErrorDTO()
                        {
                            ErrorCode = PromoErrorTypeDTO.MaxRedemptions,
                            Message = $"Promo expired"
                        });
                    }

                    if (dbCartModel.Products.Select(_product => _product.Id).Contains((Id)dbPromo.Id) == false)
                    {
                        dbCartModel.PromoErrors.Add(new PromoErrorDTO()
                        {
                            ErrorCode = PromoErrorTypeDTO.NoRelatedProduct,
                            Message = "Promo does not apply to any items in the cart"
                        });
                    }

                    dbPromo.RedemptionCount += 1;
                    db.AddOrUpdate(dbPromo);
                    db.SaveChanges();
                    
                     PromoCode promoCode = promomapper.ModelToContract(dbPromo);

                    // apply promo   
                    dbCartModel.PromoCodes.Add(dbPromo);
                    db.AddOrUpdate(dbCartModel);
                    db.SaveChanges();
                }
                else
                {
                    dbCartModel.PromoErrors.Add(new PromoErrorDTO()
                    {
                        ErrorCode = PromoErrorTypeDTO.InvalidPromo,
                        Message = $"Promo {code} doesn't exist"
                    });
                }
            }

            Cart cartSaved = mapper.ModelToContract(dbCartModel);
            cartResult = new SaveResult<Cart>(cartSaved);
            return cartResult;

        }

        public SaveResult<CartDiscountResult> CalculateDiscounts(Cart cart)
        {
            SaveResult<CartDiscountResult> cartDiscountResult;
            CartDiscountResult cartDiscount;
            CartDTO dbCartModel = mapper.ContractToModel(cart);

            using (TestingHomeworkContext db = new TestingHomeworkContext())
            {

                double originalPrice = dbCartModel.Products.Sum(_product => _product.Price);

                double calculatedPrice = originalPrice;
                List<ProductDiscountResultDTO> productDiscounts = new List<ProductDiscountResultDTO>();
                foreach (var promo in dbCartModel.PromoCodes)
                {
                    if (promo.Product != null)
                    {
                        var discountedPrice = EnforceZeroMinimum(promo.Product.Price - promo.DollarDiscount);
                        productDiscounts.Add(new ProductDiscountResultDTO()
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

                CartDiscountResultDTO CartDiscountResultModel = new CartDiscountResultDTO
                {
                    OriginalPrice = originalPrice,
                    FinalPrice = finalPrice,
                    ProductDiscounts = productDiscounts,
                };

                cartDiscount = cartDiscountResultMapper.ModelToContract(CartDiscountResultModel);
            }
            cartDiscountResult = new SaveResult<CartDiscountResult>(cartDiscount);
            return cartDiscountResult;
        }

        private double EnforceZeroMinimum(double price)
        {
            if (price >= 0)
            {
                return price;
            }
            else
            {
                return 0;
            }
        }

        public SaveResult<Cart> GetUserCart(Guid userId)
        {
            SaveResult<Cart> cartResult;
            using (TestingHomeworkContext db = new TestingHomeworkContext())
            {
                CartDTO userCart = db.Carts
                    .Include(_cart => _cart.User)
                    .FirstOrDefault(_cart => _cart.User.Id == userId);

                userCart.Products = db.CartProducts.Include(cp => cp.Product).Where(cp => cp.CartId == userCart.Id).Select(cp => cp.Product).ToList();
                userCart.PromoCodes = db.CartPromos.Include(cp => cp.PromoCode).Where(cp => cp.CartId == userCart.Id).Select(cp => cp.PromoCode).ToList();

                Cart cart = mapper.ModelToContract(userCart);
                cartResult = new SaveResult<Cart>(cart);
            }
            return cartResult;
        }

        public SaveResult<Cart> SaveCart(Cart cartSave)
        {

            using (TestingHomeworkContext db = new TestingHomeworkContext())
            {
                CartDTO cart = mapper.ContractToModel(cartSave);

                var productCache = cart.Products.ToList();
                var promoCache = cart.PromoCodes.ToList();
                cart.Products = new List<ProductDTO>();
                cart.PromoCodes = new List<PromoCodeDTO>();

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

                var toAddCartProductModels = toAddProductIds.Select(productId => new CartProductDTO() { CartId = cart.Id, ProductId = productId });
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

                var toAddCartPromoModels = toAddPromoIds.Select(promoId => new CartPromoDTO() { CartId = cart.Id, PromoCodeId = promoId });
                db.CartPromos.AddRange(toAddCartPromoModels);

                foreach (var toRemoveId in toRemoveProductIds)
                {
                    db.CartPromos.Remove(db.CartPromos.First(cp => cp.CartId == cart.Id && cp.PromoCodeId == toRemoveId));
                }

                db.SaveChanges();
                cart.Products = productCache;
                cart.PromoCodes = promoCache;

                Cart savedCart = mapper.ModelToContract(cart);

                SaveResult<Cart> saveResult = new SaveResult<Cart>(savedCart);
                return saveResult;
            }

        }


    }
}
