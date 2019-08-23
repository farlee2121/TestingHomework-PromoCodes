using Accessors.DatabaseAccessors;
using DeepEqual.Syntax;
using Managers.LazyCollectionOfAllManagers;
using Ninject;
using NUnit.Framework;
using Shared.DataContracts;
using Shared.DependencyInjectionKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock.AutoMock;
using Test.NUnitExtensions;
using Tests.DataPrep;
using Tests.ManagerTests;

namespace TestingHomework.Tests
{
    //[TestFixture_Prefixed(typeof(PromoAdminManager), false)]
    [TestFixture_Prefixed(typeof(PromoAdminManager), true)]
    public class PromoAdminManagerTests : ManagerTestBase
    {
        PromoAdminManager manager;
        MockingContainer<PromoAdminManager> mockContainer = new MockingContainer<PromoAdminManager>();

        public PromoAdminManagerTests() : this(false) { }
        public PromoAdminManagerTests(bool isIntegration = false)
        {
            // this constructor allows for integration test reuse
            if (isIntegration)
            {
                // Implicit self binding allows us to get a concrete class with fulfilled dependencies
                // https://github.com/ninject/ninject/wiki/dependency-injection-with-ninject
                Ninject.IKernel kernel = DependencyInjectionLoader.BuildKernel();
                manager = kernel.Get<PromoAdminManager>();
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
        public void GetAllPromoCodes()
        {
            // arrange
            Product product = dataPrep.Products.Create();

            int expectedItemCount = 5;
            IEnumerable<PromoCode> expectedPromoCodes = dataPrep.Promocodes.CreateManyForList(expectedItemCount, product);

            mockContainer.Arrange<IPromoAccessor>(accessor => accessor.GetAllPromos(product.Id)).Returns(expectedPromoCodes);

            //// act
            IEnumerable<PromoCode> actualPromoCodes = manager.GetAllPromos(product.Id);

            ////assert
            Assert.AreEqual(expectedPromoCodes.Count(), actualPromoCodes.Count());
            expectedPromoCodes.ShouldDeepEqual(actualPromoCodes);
        }

        [Test]
        public void SavePromo()
        {
            // arrange
            PromoCode expectedPromoCode = dataPrep.Promocodes.CreateData(isPersisted: false);

            mockContainer.Arrange<IPromoAccessor>(accessor => accessor.SavePromo(expectedPromoCode)).Returns(new SaveResult<PromoCode>(expectedPromoCode));

            // act
            SaveResult<PromoCode> actualPromoCodeResult = manager.SavePromo(expectedPromoCode);
            PromoCode actualPromoCode = actualPromoCodeResult.Result;

            //assert
            //Assert.False(actualProduct.Id == Guid.Empty);
            Assert.IsTrue(actualPromoCodeResult.Success);
            expectedPromoCode.WithDeepEqual(actualPromoCode).IgnoreSourceProperty((p) => p.Id);
        }

        [Test]
        public void UpdatePromo()
        {
            // arrange
            PromoCode expectedPromoCode = dataPrep.Promocodes.CreateData();
            expectedPromoCode.Code = Guid.NewGuid().ToString();
            
            mockContainer.Arrange<IPromoAccessor>(accessor => accessor.SavePromo(expectedPromoCode)).Returns(new SaveResult<PromoCode>(expectedPromoCode));

            // act
            SaveResult<PromoCode> actualPromoCodeResult = manager.SavePromo(expectedPromoCode);
            PromoCode actualPromoCode = actualPromoCodeResult.Result;

            //assert
            Assert.IsTrue(actualPromoCodeResult.Success);
            Assert.AreEqual(expectedPromoCode.Id, actualPromoCode.Id);
            expectedPromoCode.WithDeepEqual(actualPromoCode);
        }
    }       
}
