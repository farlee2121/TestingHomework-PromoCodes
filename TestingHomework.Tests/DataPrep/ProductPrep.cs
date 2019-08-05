using System;
using System.Collections.Generic;
using System.Text;
using TestingHomework.Tests.Data;
using TestingHomework_Discounts;

namespace TestingHomework.Tests.DataPrep
{
    public class ProductPrep : TypePrepBase<Product, Data.ProductDTO>
    {
        public ProductPrep(ITestDataAccessor dataAccessor) : base(dataAccessor)
        {
           
        }       

        public IEnumerable<Product> CreateManyForProducts(int count, Product product)
        {
            List<Product> products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                Product productList = Create(product);
                products.Add(productList);
            }

            return products;
        }
        public override Product  Create(Product product = null, bool isPersisted = true)
        {        
            Product sanitizedProduct = product ?? Create();
            Product productItem = new Product()
            { 
                Id = sanitizedProduct.Id,
                Description = Faker.Lorem.Sentence(),
                Price = Faker.RandomNumber.Next(5)
            };

            if (isPersisted)
            {
                Product savedProduct = base.Create(productItem);
                return savedProduct;
            }
            else
            {
                return productItem;
            }

        }
    }
}