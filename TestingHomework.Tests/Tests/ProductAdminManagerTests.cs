using System;
using System.Collections.Generic;
using System.Text;

using TestingHomework_Discounts.Managers;
using TestingHomework_Discounts;
using System.Linq;
using TestingHomework.Tests.Data;
using TestingHomework.Tests.DataPrep;
using NUnit.Framework;
using TestingHomework_Discounts.Models;
using DeepEqual.Syntax;
using TestingHomework.Tests.TestBases;
using Moq;
using Telerik.JustMock.AutoMock;
using TestingHomework.Tests.Accessors;

namespace TestingHomework.Tests
{
    public class ProductAdminManagerTests: ManagerTestBase
    {
        MockingContainer<ProductAdminManager> mockContainer = new MockingContainer<ProductAdminManager>();

        ProductAdminManager productAdminManager;
 
        public override void OnInitialize()
        {

            productAdminManager = mockContainer.Instance;
        }

        [Test]
        public void SaveProduct()
        {
            var product = dataPrep.Products.Create();
            mockContainer.Arrange<IProductAdminAccessor>(accessor => accessor.SaveProduct(product)).Returns(product);
            productAdminManager = mockContainer.Instance;
            var savedProduct = productAdminManager.SaveProduct(product);
           
            product.ShouldDeepEqual(savedProduct);
        }

        [Test]
        public void GetAllProducts()
        {
            // arrange
            var productList = dataPrep.Products.Create();
            int expectedItemCount = 5;
           var expectedItemList = dataPrep.Products.CreateManyForProducts(expectedItemCount, productList).ToList();
            mockContainer.Arrange<IProductAdminAccessor>(accessor => accessor.GetAllProducts()).Returns(expectedItemList);
                        
            productAdminManager = mockContainer.Instance;

            var products = productAdminManager.GetAllProducts().ToList();

            expectedItemList.ShouldDeepEqual(products);
            

        }

        [Test]
        public void GetProductById()
        {
            // arrange
            var product = dataPrep.Products.Create();
            mockContainer.Arrange<IProductAdminAccessor>(accessor => accessor.GetProduct(product.Id)).Returns(product);

            productAdminManager = mockContainer.Instance;

            var expectedProduct = productAdminManager.GetProduct(product.Id);

            product.ShouldDeepEqual(expectedProduct);


        }

        public override void OnCleanup()
        {
    
        }

     

    }
}
