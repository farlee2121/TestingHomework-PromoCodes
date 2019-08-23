using Accessors.DatabaseAccessors;
using DeepEqual.Syntax;
using Managers.LazyCollectionOfAllManagers;
using Ninject;
using NUnit.Framework;
using Shared.DataContracts;
using Shared.DependencyInjectionKernel;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock.AutoMock;
using Test.NUnitExtensions;
using Tests.DataPrep;
using Tests.ManagerTests;

namespace TestingHomework.Tests
{
    //[TestFixture_Prefixed(typeof(CheckoutManager), false)]
    [TestFixture_Prefixed(typeof(CheckoutManager), true)]
    public class CheckoutManagerTests : ManagerTestBase
    {
        CheckoutManager manager;
        MockingContainer<CheckoutManager> mockContainer = new MockingContainer<CheckoutManager>();

        public CheckoutManagerTests() : this(false) { }
        public CheckoutManagerTests(bool isIntegration = false)
        {
            // this constructor allows for integration test reuse
            if (isIntegration)
            {
                // Implicit self binding allows us to get a concrete class with fulfilled dependencies
                // https://github.com/ninject/ninject/wiki/dependency-injection-with-ninject
                Ninject.IKernel kernel = DependencyInjectionLoader.BuildKernel();
                manager = kernel.Get<CheckoutManager>();
                dataPrep = new DiscountsDataPrep(true);
            }
            else
            {
                manager = mockContainer.Instance;
                // dataPrep non-persistant by default in base class
            }
        }

        public override void OnCleanup()
        {

        }
        

        public override void OnInitialize()
        {

        }

        [Test]
        public void RedeemPromo()
        {        
            // arrange
            IEnumerable<Product> products = dataPrep.Products.CreateManyForList(1, false);

            PromoCode promoCode = dataPrep.Promocodes.CreateData(products.FirstOrDefault(),true);
            ICollection<PromoCode> promoCodes = new List<PromoCode>();
            promoCodes.Add(promoCode);
            Cart cart = dataPrep.Carts.CreateData(products, null, null, isPersisted: true);

            mockContainer.Arrange<ICheckoutAccessor>(accessor => accessor.RedeemPromo(promoCode.Code,cart)).Returns(new SaveResult<Cart>(cart));

            // act
            SaveResult<Cart> actualUserResult = manager.RedeemPromo(promoCode.Code,cart);
            Cart actualCart = actualUserResult.Result;

            //assert
            Assert.IsTrue(actualUserResult.Success);
            actualCart.PromoCodes.Contains(promoCode);
        }



        [Test]
        public void CalculateDiscounts()
        {
            //// arrange           
            Cart cart = dataPrep.Carts.CreateData(null, null, null, isPersisted: true);

            mockContainer.Arrange<ICheckoutAccessor>(accessor => accessor.CalculateDiscounts(cart)).Returns(cart);
            ////// act
            SaveResult<CartDiscountResult> actualCartDiscountResult = manager.CalculateDiscounts(cart);
            CartDiscountResult actualCartDiscount = actualCartDiscountResult.Result;

            //////assert
            Assert.IsTrue(actualCartDiscountResult.Success);
        }


        [Test]
        public void GetUserCart()
        {
            //// arrange           
            Cart cart = dataPrep.Carts.CreateData(null, null, null, isPersisted: true);
            Product products = dataPrep.Products.CreateData(false);
            CartProduct cartProduct = dataPrep.CartProducts.CreateData(products, cart);

            Product productforPromo = dataPrep.Products.CreateData(false);
            PromoCode promoCode = dataPrep.Promocodes.CreateData(productforPromo,false);

            CartPromo cartPromo = dataPrep.CartPromos.CreateData(promoCode, cart);

            mockContainer.Arrange<ICheckoutAccessor>(accessor => accessor.GetUserCart(cart.UserId)).Returns(cart.UserId);
            ////// act
            SaveResult<Cart> actualCartResult = manager.GetUserCart(cart.UserId);
            Cart actualCart = actualCartResult.Result;

            //////assert
            Assert.IsTrue(actualCartResult.Success);
        }

        [Test]
        public void SaveCart()
        {
            // arrange
            Cart expectedCart = dataPrep.Carts.CreateData(isPersisted: false);

            mockContainer.Arrange<ICheckoutAccessor>(accessor => accessor.SaveCart(expectedCart)).Returns(new SaveResult<Cart>(expectedCart));

            // act
            SaveResult<Cart> actualCartResult = manager.SaveCart(expectedCart);
            Cart actualCart = actualCartResult.Result;

            //assert
            Assert.IsTrue(actualCartResult.Success);
            expectedCart.WithDeepEqual(actualCart).IgnoreSourceProperty((p) => p.Id);
        }
    }
}
