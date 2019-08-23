using Microsoft.EntityFrameworkCore;
using Shared.DatabaseContext;
using Shared.DatabaseContext.DBOs;
using Shared.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accessors.DatabaseAccessors
{
    public interface IProductAccessor
    {
        Product GetProduct(Id productId);
        IEnumerable<Product> GetAllProducts();
        SaveResult<Product> SaveProduct(Product product);
        DeleteResult DeleteProduct(Id Id);
    }
    public class ProductAccessor : IProductAccessor
    {
        Product_Mapper mapper = new Product_Mapper();

        public Product GetProduct(Id productId)
        {
            Product product;
            using (TestingHomeworkContext db = new TestingHomeworkContext())
            {
                ProductDTO productModel = db.Products.Where(ti =>ti.Id == productId && ti.IsActive).FirstOrDefault();
                product = mapper.ModelToContract(productModel);
            }
            return product;
        }


        public IEnumerable<Product> GetAllProducts()
        {
            IEnumerable<Product> productList;
            using (TestingHomeworkContext db = new TestingHomeworkContext())
            {
                IEnumerable<ProductDTO> productModellList = db.Products.Where(ti => ti.IsActive).ToList();
                productList = mapper.ModelListToContractList(productModellList);
            }
            return productList;
        }

        public SaveResult<Product> SaveProduct(Product product)
        {
            using (TestingHomeworkContext db = new TestingHomeworkContext())
            {
                ProductDTO dbModel = mapper.ContractToModel(product);

                db.AddOrUpdate(dbModel);
                db.SaveChanges();

                Product savedProduct = mapper.ModelToContract(dbModel);

                SaveResult<Product> saveResult = new SaveResult<Product>(savedProduct);
                return saveResult;
            }
        }
        public DeleteResult DeleteProduct(Id Id)
        {
            using (TestingHomeworkContext db = new TestingHomeworkContext())
            {
                ProductDTO productModel = db.Products.FirstOrDefault(ti => ti.Id == Id);
                productModel.IsActive = false;

                db.SaveChanges();

                DeleteResult deleteResult = new DeleteResult();

                return deleteResult;
            }
        }
    }
}
