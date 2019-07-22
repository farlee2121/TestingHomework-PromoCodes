//using Shared.DatabaseContext.DBOs;
//using Shared.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingHomework_Discounts;

namespace Tests.DataPrep
{
    public class ProductItemPrep : TypePrepBase<Product>
    {
        ProductListPrep ProductListPrep;

        public ProductItemPrep(ITestDataAccessor dataAccessor, ProductListPrep ProductListPrep) : base (dataAccessor)
        {
            this.ProductListPrep = ProductListPrep;
        }

        public Product CreateData(bool? isComplete = null, bool isPersisted = true)
        {

            //Product sanitizedProductList = productList ?? ProductListPrep.Create();
            Product ProductItem = new Product()
            {
                Description = random.Lorem.Sentence(),
                Price = Convert.ToDouble(random.Finance.Amount())
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


        public IEnumerable<Product> CreateManyForList(int count)
        {
            List<Product> itemList = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                Product item = CreateData();
                itemList.Add(item);
                
            }

            return itemList;
        }
    }
}
