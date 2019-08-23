//using Shared.DatabaseContext.DBOs;
//using Shared.DataContracts;
using Shared.DatabaseContext.DBOs;
using Shared.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tests.DataPrep
{
    public class CartPromoPrep : TypePrepBase<CartPromo, CartPromoDTO>
    {
        PromoCodePrep promoCodePrep;
        CartPrep cartPrep;

        public CartPromoPrep(ITestDataAccessor dataAccessor, PromoCodePrep promoCodePrep, CartPrep CartPrep) : base(dataAccessor)
        {
            this.promoCodePrep = promoCodePrep;
            this.cartPrep = CartPrep;
        }

        public CartPromo CreateData(PromoCode promoCode = null, Cart cart = null, bool isPersisted = true)
        {
            PromoCode sanitizedPromoCode = promoCode ?? promoCodePrep.Create();
            Cart sanitizedCart = cart ?? cartPrep.Create();

            var date = Convert.ToDateTime(random.Date.Future());
            CartPromo cartPromo = new CartPromo()
            {
                CartId = sanitizedCart.Id,
                //Cart = sanitizedCart,
                PromoCodeId = sanitizedPromoCode.Id,
                PromoCode= sanitizedPromoCode,
            };

            if (isPersisted)
            {
                CartPromo savedItem = Create(cartPromo);
                return savedItem;
            }
            else
            {
                return cartPromo;
            }
        }


        public IEnumerable<CartPromo> CreateManyForList(int count, PromoCode promoCode = null, Cart cart = null, bool isPersisted = true)
        {
            List<CartPromo> itemList = new List<CartPromo>();
            for (int i = 0; i < count; i++)
            {
                CartPromo item = CreateData(promoCode, cart, isPersisted);
                itemList.Add(item);
            }
            return itemList;
        }
    }
}
