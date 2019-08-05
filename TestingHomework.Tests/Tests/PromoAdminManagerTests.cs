using DeepEqual.Syntax;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Telerik.JustMock.AutoMock;
using TestingHomework.Tests.TestBases;
using TestingHomework_Discounts.Accessors;
using TestingHomework_Discounts.Managers;

namespace TestingHomework.Tests.Tests
{
    public class PromoAdminManagerTests: ManagerTestBase
    {
        MockingContainer<PromoAdminManager> mockContainer = new MockingContainer<PromoAdminManager>();

        PromoAdminManager promoAdminManager;

        public override void OnCleanup()
        {
            throw new NotImplementedException();
        }

        public override void OnInitialize()
        {
            promoAdminManager = mockContainer.Instance;
        }

        [Test]
        public void SavePromo()
        {
            var promocode = dataPrep.PromoCodes.Create();
            mockContainer.Arrange<IPromoAdminAccessor>(accessor => accessor.SavePromo(promocode)).Returns(promocode);

            promoAdminManager = mockContainer.Instance;

            var expectedPromocode = promoAdminManager.SavePromo(promocode);

            promocode.ShouldDeepEqual(expectedPromocode);
        }

        [Test]
        public void GetAllPromos()
        {           
            var promocode = dataPrep.PromoCodes.Create();
            int expectedItemCount = 5;
            var expectedItemList = dataPrep.PromoCodes.CreateManyForPromocodes(expectedItemCount, promocode);
            mockContainer.Arrange<IPromoAdminAccessor>(accessor => accessor.GetAllPromos()).Returns(expectedItemList);

            promoAdminManager = mockContainer.Instance;

            var promocodes = promoAdminManager.GetAllPromos();

            expectedItemList.ShouldDeepEqual(promocodes);


        }
    }
}
