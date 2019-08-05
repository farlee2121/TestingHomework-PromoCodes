using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TestingHomework_Discounts.Accessors;

namespace TestingHomework_Discounts.Managers
{
    public interface ICheckoutManager
    {
        Cart RedeemPromo(string code, Cart cart);
        Cart GetUserCart(Guid userId);
        Cart SaveCart(Cart cart);
        CartDiscountResult CalculateDiscounts(Cart cart);

    }  
       
        public class CheckoutManager: ICheckoutManager
        {
            ICheckoutAccessor checkoutAccessor;
            public CheckoutManager(ICheckoutAccessor checkoutAccessor)
            {
                this.checkoutAccessor = checkoutAccessor;
            }

            public Cart RedeemPromo(string code, Cart cart)
        {          
                return checkoutAccessor.RedeemPromo(code,cart);            
        }

        public Cart GetUserCart(Guid userId)
        {
            return checkoutAccessor.GetUserCart(userId);
        }

        public Cart SaveCart(Cart cart)
        {
            return checkoutAccessor.SaveCart(cart);
        }

        public CartDiscountResult CalculateDiscounts(Cart cart)
        {
            return checkoutAccessor.CalculateDiscounts(cart);
        }


    }
}
