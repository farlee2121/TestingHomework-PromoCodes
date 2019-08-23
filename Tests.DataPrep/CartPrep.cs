
using Shared.DatabaseContext.DBOs;
using Shared.DataContracts;
using System.Collections.Generic;
using System.Linq;


namespace Tests.DataPrep
{
    public class CartPrep : TypePrepBase<Cart, CartDTO>
    {
        ProductPrep ProductPrep;
        UserPrep UserPrep;
        PromoCodePrep PromoCodePrep;
        PromoErrorPrep PromoErrorPrep;

        public CartPrep(ITestDataAccessor dataAccessor, ProductPrep ProductPrep, UserPrep UserPrep, PromoCodePrep promoCodePrep, PromoErrorPrep promoErrorPrep) : base (dataAccessor)
        {
           this.ProductPrep = ProductPrep;
           this.UserPrep = UserPrep;
           this.PromoCodePrep = promoCodePrep;
           this.PromoErrorPrep = promoErrorPrep;
        }

        public Cart CreateData(IEnumerable<Product> products = null,User user = null, ICollection<PromoCode> promoCode = null,bool isPersisted = true)
        {
            IEnumerable<Product> sanitizedProducts = products ?? ProductPrep.CreateManyForList(1,false);

            User sanitizedUser = user ?? UserPrep.CreateData(null,false);

            Product product = sanitizedProducts.FirstOrDefault();

            ICollection<PromoCode> sanitizedPromoCode = promoCode ?? PromoCodePrep.CreateManyForList(1, product,false).ToList();

            Cart cart = new Cart()
            {
               Products = sanitizedProducts,
               UserId = sanitizedUser.Id,
               User = sanitizedUser,
               PromoCodes = sanitizedPromoCode,        
            };

            if (isPersisted)
            {
                Cart savedItem = Create(cart);
                return savedItem;
            }
            else
            {
                return cart;
            }
        }


        public IEnumerable<Cart> CreateManyForList(int count,bool isPersisted = true)
        {
            List<Cart> itemList = new List<Cart>();
            for (int i = 0; i < count; i++)
            {
                Cart item = CreateData(null,null,null,isPersisted);
                itemList.Add(item);                
            }
            return itemList;
        }
    }
}
