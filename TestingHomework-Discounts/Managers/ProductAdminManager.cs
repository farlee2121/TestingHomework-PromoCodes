using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestingHomework.Tests.Accessors;

namespace TestingHomework_Discounts.Managers
{
    public interface IProductAdminManager
    {
        IEnumerable<Product> GetAllProducts();

        Product SaveProduct(Product product);

        Product GetProduct(Guid productId);
    }
    public class ProductAdminManager: IProductAdminManager
    {

        IProductAdminAccessor productAdminAccessor;
        public ProductAdminManager(IProductAdminAccessor productAdminAccessor)
        {
            this.productAdminAccessor = productAdminAccessor;
        }


        public IEnumerable<Product> GetAllProducts()
        {
            using (PromoRepository db = new PromoRepository())
            {
                return productAdminAccessor.GetAllProducts();
            }
        }


        public Product SaveProduct(Product product)
        {
            using (PromoRepository db = new PromoRepository())
            {
                return productAdminAccessor.SaveProduct(product);
            }

           
        }

        public Product GetProduct(Guid productId)
        {
            using (PromoRepository db = new PromoRepository())
            {
                return productAdminAccessor.GetProduct(productId);
            }
        }
    }
}
