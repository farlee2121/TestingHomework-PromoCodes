using DeepEqual.Syntax;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.JustMock.AutoMock;
using TestingHomework.Tests.Data;
using TestingHomework.Tests.TestBases;
using TestingHomework_Discounts;
using TestingHomework_Discounts.Accessors;
using TestingHomework_Discounts.Managers;
using Xunit;

namespace TestingHomework.Tests
{
  
    public class CheckoutManagerTests : ManagerTestBase
    {
        MockingContainer<CheckoutManager> mockContainer = new MockingContainer<CheckoutManager>();

        CheckoutManager checkoutManager;

        public override void OnCleanup()
        {
            throw new NotImplementedException();
        }

        public override void OnInitialize()
        {

            checkoutManager = mockContainer.Instance;
        }


        [Test]
        public void GetUserCart_NoCart()
        {      
            mockContainer.Arrange<ICheckoutAccessor>(accessor => accessor.GetUserCart(Guid.Empty));
            checkoutManager = mockContainer.Instance;
            var userCart = checkoutManager.GetUserCart(Guid.Empty);
            NUnit.Framework.Assert.Null(userCart);
        }


        [Test]
        public void GetUserCart_Success()
        {
            Cart expectedCart = dataPrep.Carts.Create();
            mockContainer.Arrange<ICheckoutAccessor>(accessor => accessor.GetUserCart(expectedCart.Id)).Returns(expectedCart);
            checkoutManager = mockContainer.Instance;
            var userCart = checkoutManager.GetUserCart(expectedCart.Id);
            expectedCart.ShouldDeepEqual(userCart);
        }

        [Test]
        public void SaveUserCart_Save()
        {
            Cart expectedCart = dataPrep.Carts.Create();
            mockContainer.Arrange<ICheckoutAccessor>(accessor => accessor.SaveCart(expectedCart)).Returns(expectedCart);
            checkoutManager = mockContainer.Instance;
            Cart savedCart = checkoutManager.SaveCart(expectedCart);
            expectedCart.ShouldDeepEqual(savedCart);
        }


        [NUnit.Framework.Test]
        public void CalculateDiscounts()
        {
            NUnit.Framework.Assert.Inconclusive("Untested: untested as an example of code marking. These markers are usually reserved for legacy code or unverifiable external dependencies");
               
        }
        

        [Test]
        public void RedeemPromo_Success()
        {
            Cart expectedCart = dataPrep.Carts.Create();
            mockContainer.Arrange<ICheckoutAccessor>(accessor => accessor.RedeemPromo("20ff", expectedCart)).Returns(expectedCart);
            checkoutManager = mockContainer.Instance;
            Cart cart = checkoutManager.RedeemPromo("20ff", expectedCart);
            Cart savedCart = checkoutManager.SaveCart(expectedCart);

        }        
    }
}
