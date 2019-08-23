
using Shared.DatabaseContext.DBOs;
using Shared.DataContracts;
using System;
using System.Collections.Generic;


namespace Tests.DataPrep
{
    public class ProductPrep : TypePrepBase<Product, ProductDTO>
    {

        public ProductPrep(ITestDataAccessor dataAccessor) : base (dataAccessor)
        {
            
        }

        public Product CreateData(bool isPersisted = true)
        {            
            Product ProductItem = new Product()
            {
                Description = random.Lorem.Sentence(),
                Price = Convert.ToDouble(random.Commerce.Price())
            };

            if (isPersisted)
            {
                Product savedItem = Create(ProductItem);
                return savedItem;
            }
            else
            {
                return ProductItem;
            }
        }


        public IEnumerable<Product> CreateManyForList(int count,bool isPersisted = true)
        {
            List<Product> itemList = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                Product item = CreateData(isPersisted);
                itemList.Add(item);
                
            }

            return itemList;
        }
    }
}
