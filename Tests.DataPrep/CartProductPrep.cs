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
    public class CartProductPrep : TypePrepBase<CartProduct, CartProductDTO>
    {
       ProductPrep ProductPrep;
       CartPrep CartPrep;

        public CartProductPrep(ITestDataAccessor dataAccessor, ProductPrep ProductPrep, CartPrep CartPrep) : base (dataAccessor)
        {
            this.ProductPrep = ProductPrep;
            this.CartPrep = CartPrep;
        }

        public CartProduct CreateData(Product product = null, Cart cart = null, bool isPersisted = true)
        {
            Product sanitizedProduct = product ?? ProductPrep.Create();
            Cart sanitizedCart = cart ?? CartPrep.Create();

            var date = Convert.ToDateTime(random.Date.Future());
            CartProduct cartProduct = new CartProduct()
            {
                CartId = sanitizedCart.Id,
                //Cart = sanitizedCart,
                ProductId = sanitizedProduct.Id,
                Product= sanitizedProduct,
            };

            if (isPersisted)
            {
                CartProduct savedItem = Create(cartProduct);
                return savedItem;
            }
            else
            {
                return cartProduct;
            }
        }


        public IEnumerable<CartProduct> CreateManyForList(int count, Product product = null, Cart cart = null, bool isPersisted = true)
        {
            List<CartProduct> itemList = new List<CartProduct>();
            for (int i = 0; i < count; i++)
            {
                CartProduct item = CreateData(product,cart,isPersisted);
                itemList.Add(item);                
            }
            return itemList;
        }
    }
}
