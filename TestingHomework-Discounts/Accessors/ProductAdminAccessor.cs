using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestingHomework.Tests.Data;
using TestingHomework_Discounts;

namespace TestingHomework.Tests.Accessors
{
    public interface IProductAdminAccessor
    {
        IEnumerable<Product> GetAllProducts();

        Product SaveProduct(Product product);

        Product GetProduct(Guid productId);
    }
    public class ProductAdminAccessor : IProductAdminAccessor
    {


        Product_Mapper mapper = new Product_Mapper();

        public IEnumerable<Product> GetAllProducts()
        {
            using (PromoRepository db = new PromoRepository())
            {
                IEnumerable<ProductDTO> productList = db.Products.ToList();

                return mapper.ModelListToContractList(productList);
            }
        }


        public Product SaveProduct(Product product)
        {
            using (PromoRepository db = new PromoRepository())
            {
                ProductDTO dbModel = mapper.ContractToModel(product);
                db.AddOrUpdate(dbModel);
                db.SaveChanges();
                 Product  savedProduct = mapper.ModelToContract(dbModel);
                return savedProduct;
            }           

        }

        public Product GetProduct(Guid productId)
        {
            using (PromoRepository db = new PromoRepository())
            {
                var product =  db.Products.FirstOrDefault(_prod => _prod.Id == productId);
                var expectedProduct = mapper.ModelToContract(product);
                return expectedProduct;


            }
        }
        
    }
}
