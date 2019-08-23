using Accessors.DatabaseAccessors;
using Shared.DataContracts;
using System.Collections.Generic;

namespace Managers.LazyCollectionOfAllManagers
{

    public interface IProductAdminManager
    {
        Product GetProduct(Id productId);

        IEnumerable<Product> GetAllProducts();

        SaveResult<Product> SaveProduct(Product product);

        DeleteResult DeleteProduct(Id id);
    }

    internal class ProductAdminManager : IProductAdminManager
    {
        IProductAccessor productAccessor;

        public ProductAdminManager(IProductAccessor productAccessor)
        {            
            this.productAccessor = productAccessor;
        }

        public Product GetProduct(Id productId)
        {
            return productAccessor.GetProduct(productId);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return productAccessor.GetAllProducts();           
        }

        public SaveResult<Product> SaveProduct(Product product)
        {
            return productAccessor.SaveProduct(product);
        }

        public DeleteResult DeleteProduct(Id id)
        {
             return productAccessor.DeleteProduct(id);           
        }
    }
}
