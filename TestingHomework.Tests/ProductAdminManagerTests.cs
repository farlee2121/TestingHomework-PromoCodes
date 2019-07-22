using System;
using System.Collections.Generic;
using System.Text;
//using Xunit;
using TestingHomework_Discounts.Managers;
using TestingHomework_Discounts;
using System.Linq;





using DeepEqual;
using DeepEqual.Syntax;
using NUnit.Framework;

using Test.NUnitExtensions;
using Tests;

namespace TestingHomework.Tests
{
    
    public class ProductAdminManagerTests : AccessorTestBase
    {
        ProductAdminManager manager = new ProductAdminManager();

        [Test]
        public void GetProduct_NoProduct()
        {

            var actualEmpty = manager.GetProduct(Guid.Empty);
            Assert.Null(actualEmpty);

        }

        [Test]
        public void GetProduct_Success()
        {
            var savedProduct = manager.SaveProduct(new Product()
            {
                Description = "asdfas",
                Price = 10.50
            });

            var actualProduct = manager.GetProduct(savedProduct.Id);
            Assert.Equals(savedProduct.Id, actualProduct.Id);
            Assert.Equals(savedProduct.Description, actualProduct.Description);
            Assert.Equals(savedProduct.Price, actualProduct.Price);

        }

        [Test]
        public void SaveProduct_NewProduct()
        {
            var savedProduct = manager.SaveProduct(new Product()
            {
                Description = "asdfas",
                Price = 10.50
            });

            Assert.False(savedProduct.Id == Guid.Empty);
            Assert.AreEqual("asdfas", savedProduct.Description);
            Assert.AreEqual(10.50, savedProduct.Price);
        }

        [Test]
        public void SaveProduct_UpdateProduct()
        {
            var existingProduct = manager.SaveProduct(new Product()
            {
                Description = "asdfas",
                Price = 10.50
            });

            existingProduct.Price = 1.00;

            var updatedProduct = manager.SaveProduct(existingProduct);

            Assert.AreEqual(existingProduct.Id, updatedProduct.Id);
            Assert.AreEqual("asdfas", existingProduct.Description);
            Assert.AreEqual(1.00, existingProduct.Price);
        }

        [Test]
        public void GetAllProducts()
        {
            List<Product> expectedProducts = new List<Product>();
            expectedProducts.Add(manager.SaveProduct(new Product()
            {
                Description = "asdfas",
                Price = 10.50
            }));

            expectedProducts.Add(manager.SaveProduct(new Product()
            {
                Description = "asdfas",
                Price = 11
            }));

            expectedProducts.Add(manager.SaveProduct(new Product()
            {
                Description = "asdfas",
                Price = 1
            }));

            var actualProductList = manager.GetAllProducts();


            Assert.AreEqual(expectedProducts.Count, actualProductList.Count());
            foreach (Product expectedProduct in actualProductList)
            {
                var actualProduct = actualProductList.FirstOrDefault(_product => _product.Id == expectedProduct.Id);
                Assert.NotNull(actualProduct);
                Assert.AreEqual(expectedProduct.Id, actualProduct.Id);
                Assert.AreEqual(expectedProduct.Description, actualProduct.Description);
                Assert.AreEqual(expectedProduct.Price, actualProduct.Price);
            }
        }



        /// <summary>
        /// For Review
        /// </summary>
        [Test]
        public void GetProduct_Success_Final()
        {
            //// arrange           
            Product expectedItem = dataPrep.ProductItems.CreateData();
            //// act
            Product actualItem = manager.GetProduct(expectedItem.Id);
            ////assert
            expectedItem.WithDeepEqual(actualItem).Assert();
        }

        [Test]
        public void GetAllProducts_Final()
        {
            //// arrange
            int expectedItemCount = 5;
            IEnumerable<Product> expectedItemList = dataPrep.ProductItems.CreateManyForList(expectedItemCount);

            //// act
            IEnumerable<Product> actualItemList = manager.GetAllProducts();

            ////assert
            expectedItemList.WithDeepEqual(actualItemList).Assert();
        }

        [Test]
        public void SaveProduct_NewProduct_Final()
        {
            // arrange
            Product expectedItem = dataPrep.ProductItems.CreateData(isPersisted: false);

            // act
            Product actualItem = manager.SaveProduct(expectedItem);

            //assert
            Assert.IsNotNull(actualItem.Id);
            expectedItem.WithDeepEqual(actualItem).IgnoreSourceProperty((ti) => ti.Id);
        }


        [Test]
        public void SaveProduct_UpdateProduct_Final()
        {
            // arrange
            Product expectedItem = dataPrep.ProductItems.CreateData();
            expectedItem.Description = Guid.NewGuid().ToString();
            // act
            Product actualItem = manager.SaveProduct(expectedItem);

            ////assert
            expectedItem.ShouldDeepEqual(actualItem);
        }
    }
}
