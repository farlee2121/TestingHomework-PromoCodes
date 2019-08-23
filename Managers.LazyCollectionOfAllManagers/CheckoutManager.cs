using Accessors.DatabaseAccessors;
using Shared.DataContracts;
using System;

namespace Managers.LazyCollectionOfAllManagers
{
    public interface ICheckoutManager
    {
        SaveResult<Cart> RedeemPromo(string code, Cart cart);

        SaveResult<CartDiscountResult> CalculateDiscounts(Cart cart);

        SaveResult<Cart> GetUserCart(Guid userId);

        SaveResult<Cart> SaveCart(Cart cart);
    }

    public class CheckoutManager: ICheckoutManager
    {
        ICheckoutAccessor checkoutAccessor;

        public CheckoutManager(ICheckoutAccessor checkoutAccessor)
        {
            this.checkoutAccessor = checkoutAccessor;
        }

        public SaveResult<Cart> RedeemPromo(string code, Cart cart)
        {
           return checkoutAccessor.RedeemPromo(code,cart);
        }

        public SaveResult<CartDiscountResult> CalculateDiscounts(Cart cart)
        {
            return checkoutAccessor.CalculateDiscounts(cart);
        }

        public SaveResult<Cart> GetUserCart(Guid userId)
        {
            return checkoutAccessor.GetUserCart(userId);
        }

        public SaveResult<Cart> SaveCart(Cart cart)
        {
            return checkoutAccessor.SaveCart(cart);

        }
    }
}
