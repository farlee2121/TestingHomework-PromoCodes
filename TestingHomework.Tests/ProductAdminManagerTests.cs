using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using TestingHomework_Discounts.Managers;
using TestingHomework_Discounts;
using System.Linq;

namespace TestingHomework.Tests
{
    public class ProductAdminManagerTests
    {
        ProductAdminManager manager = new ProductAdminManager();

        [Fact]
        public void GetProduct_NoProduct()
        {

            var actualEmpty = manager.GetProduct(Guid.Empty);
            Assert.Null(actualEmpty);

        }

        [Fact]
        public void GetProduct_Success()
        {
            var savedProduct = manager.SaveProduct(new Product()
            {
                Description = "asdfas",
                Price = 10.50
            });

            var actualProduct = manager.GetProduct(savedProduct.Id);
            Assert.Equal(savedProduct.Id, actualProduct.Id);
            Assert.Equal(savedProduct.Description, actualProduct.Description);
            Assert.Equal(savedProduct.Price, actualProduct.Price);

        }

        [Fact]
        public void SaveProduct_NewProduct()
        {
            var savedProduct = manager.SaveProduct(new Product()
            {
                Description = "asdfas",
                Price = 10.50
            });

            Assert.False(savedProduct.Id == Guid.Empty);
            Assert.Equal("asdfas", savedProduct.Description);
            Assert.Equal(10.50, savedProduct.Price);
        }

        [Fact]
        public void SaveProduct_UpdateProduct()
        {
            var existingProduct = manager.SaveProduct(new Product()
            {
                Description = "asdfas",
                Price = 10.50
            });

            existingProduct.Price = 1.00;

            var updatedProduct = manager.SaveProduct(existingProduct);

            Assert.Equal(existingProduct.Id, updatedProduct.Id);
            Assert.Equal("asdfas", existingProduct.Description);
            Assert.Equal(1.00, existingProduct.Price);
        }

        [Fact]
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


            Assert.Equal(expectedProducts.Count, actualProductList.Count());
            foreach (Product expectedProduct in actualProductList)
            {
                var actualProduct = actualProductList.FirstOrDefault(_product => _product.Id == expectedProduct.Id);
                Assert.NotNull(actualProduct);
                Assert.Equal(expectedProduct.Id, actualProduct.Id);
                Assert.Equal(expectedProduct.Description, actualProduct.Description);
                Assert.Equal(expectedProduct.Price, actualProduct.Price);
            }
        }
    }
}
