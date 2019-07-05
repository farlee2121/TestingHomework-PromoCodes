using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestingHomework_Discounts;
using TestingHomework_Discounts.Managers;
using Xunit;

namespace TestingHomework.Tests
{
    public class CheckoutManagerTests
    {
        CheckoutManager manager = new CheckoutManager();

        [Fact]
        public void GetUserCart()
        {
            UserAdminManager userManager = new UserAdminManager();
            User cartUser = userManager.SaveUser(new User()
            {
                FirstName = "Sam",
                LastName = "Willis"
            });

            ProductAdminManager productManager = new ProductAdminManager();
            List<Product> cartProducts = new List<Product>();
            cartProducts.Add(productManager.SaveProduct(new Product()
            {
                Price = 1,
                Description = "Waffles"
            }));

            cartProducts.Add(productManager.SaveProduct(new Product()
            {
                Price = .50,
                Description = "Pancakes"
            }));

            PromoAdminManager promoManager = new PromoAdminManager();
            List<PromoCode> cartPromos = new List<PromoCode>();
            cartPromos.Add(promoManager.SavePromo(new PromoCode()
            {
                Code = "1OFF",
                DollarDiscount = 1,
            }));


            Cart expectedCart = new Cart()
            {
                User = cartUser,
                Products = cartProducts,
                PromoCodes = cartPromos,
            };

            Cart savedCart = manager.SaveCart(expectedCart);

            Cart actualCart = manager.GetUserCart(cartUser.Id);

            Assert.Equal(savedCart.User.Id, actualCart.User.Id);

            Assert.Equal(expectedCart.Products.Count(), actualCart.Products.Count());
            foreach (Product expectedProduct in savedCart.Products)
            {
                var actualProduct = actualCart.Products.FirstOrDefault(actual => actual.Id == expectedProduct.Id);
                Assert.NotNull(actualProduct);
            }

            Assert.Equal(expectedCart.PromoCodes.Count(), actualCart.PromoCodes.Count());
            foreach (PromoCode expectedPromo in savedCart.PromoCodes)
            {
                var actualPromo = actualCart.PromoCodes.FirstOrDefault(actual => actual.Id == expectedPromo.Id);
                Assert.NotNull(actualPromo);
            }
        }
    }
}
