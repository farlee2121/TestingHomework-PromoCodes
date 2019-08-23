using Accessors.DatabaseAccessors;
using DeepEqual.Syntax;
using Managers.LazyCollectionOfAllManagers;
using Ninject;
using NUnit.Framework;
using Shared.DatabaseContext;
using Shared.DatabaseContext.DBOs;
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
    //[TestFixture_Prefixed(typeof(ProductAdminManager), false)]
    [TestFixture_Prefixed(typeof(ProductAdminManager), true)]
    public class ProductAdminManagerTests : ManagerTestBase
    {
        ProductAdminManager manager;
        MockingContainer<ProductAdminManager> mockContainer = new MockingContainer<ProductAdminManager>();

        public ProductAdminManagerTests() : this(false) { }
        public ProductAdminManagerTests(bool isIntegration = false)
        {
            // this constructor allows for integration test reuse
            if (isIntegration)
            {
                // Implicit self binding allows us to get a concrete class with fulfilled dependencies
                // https://github.com/ninject/ninject/wiki/dependency-injection-with-ninject
                Ninject.IKernel kernel = DependencyInjectionLoader.BuildKernel();
                manager = kernel.Get<ProductAdminManager>();
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
            using (TestingHomeworkContext db = new TestingHomeworkContext())
            {
                foreach (ProductDTO us in db.Products)
                {
                    us.IsActive = false;
                    db.SaveChanges();
                }

            }
        }

        public override void OnInitialize()
        {

        }
              
        [Test]
        public void GetProduct_Success()
        {
            //// arrange           
            Product expectedProduct = dataPrep.Products.CreateData();
            mockContainer.Arrange<IProductAccessor>(accessor => accessor.GetProduct(expectedProduct.Id)).Returns(expectedProduct);
            //// act
            Product actualProduct = manager.GetProduct(expectedProduct.Id);
            ////assert
            expectedProduct.ShouldDeepEqual(actualProduct);
        }
        
        [Test]
        public void GetAllProducts()
        {
            // arrange
            int expectedItemCount = 5;
            IEnumerable<Product> expectedProducts = dataPrep.Products.CreateManyForList(expectedItemCount);

            mockContainer.Arrange<IProductAccessor>(accessor => accessor.GetAllProducts()).Returns(expectedProducts);

            //// act
            IEnumerable<Product> actualProductList = manager.GetAllProducts();

            ////assert
            Assert.AreEqual(expectedProducts.Count(), actualProductList.Count());
            expectedProducts.ShouldDeepEqual(actualProductList);
        }

        [Test]
        public void SaveProduct_NewProduct()
        {
            // arrange
            Product expectedProduct = dataPrep.Products.CreateData(isPersisted: false);

            mockContainer.Arrange<IProductAccessor>(accessor => accessor.SaveProduct(expectedProduct)).Returns(new SaveResult<Product>(expectedProduct));

            // act
            SaveResult<Product> actualProductResult = manager.SaveProduct(expectedProduct);
            Product actualProduct = actualProductResult.Result;

            //assert
            //Assert.False(actualProduct.Id == Guid.Empty);
            Assert.IsTrue(actualProductResult.Success);
            expectedProduct.WithDeepEqual(actualProduct).IgnoreSourceProperty((p) => p.Id);
        }

        [Test]
        public void SaveProduct_UpdateProduct()
        {
            // arrange
            Product expectedProduct = dataPrep.Products.CreateData();
            expectedProduct.Description = Guid.NewGuid().ToString();

            mockContainer.Arrange<IProductAccessor>(accessor => accessor.SaveProduct(expectedProduct)).Returns(new SaveResult<Product>(expectedProduct));

            // act
            SaveResult<Product> actualProductResult = manager.SaveProduct(expectedProduct);
            Product actualProduct = actualProductResult.Result;

            //assert            
            Assert.IsTrue(actualProductResult.Success);
            Assert.AreEqual(expectedProduct.Id, actualProduct.Id);
            expectedProduct.ShouldDeepEqual(actualProduct);
        }

        [Test]
        public void DeleteProduct()
        {
            //// arrange           
            Product expectedProduct = dataPrep.Products.CreateData();
            mockContainer.Arrange<IProductAccessor>(accessor => accessor.DeleteProduct(expectedProduct.Id)).Returns(expectedProduct);
            //// act
            DeleteResult deleteProductResult = manager.DeleteProduct(expectedProduct.Id);
            ////assert
       
            Assert.IsTrue(deleteProductResult.Success);

            Product retrievableProduct = manager.GetProduct(expectedProduct.Id);
            Assert.IsNull(retrievableProduct);
        }
    }
}
