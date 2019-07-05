using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingHomework_Discounts.Managers
{
    class ProductManager
    {
        public IEnumerable<Product> GetAllProducts()
        {
            using (PromoRepository db = new PromoRepository())
            {
                return db.Products.ToList();
            }
        }

        public Product SaveProduct(Product product)
        {
            using (PromoRepository db = new PromoRepository())
            {
                if (product.Id.IsDefault())
                {
                    db.Products.Add(product);
                }
                else
                {
                    db.Products.Attach(product);
                    db.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                db.SaveChanges();
            }

            return product;
        }

        public Product GetProduct(Id productId)
        {
            using (PromoRepository db = new PromoRepository())
            {
                return db.Products.FirstOrDefault(_prod => _prod.Id == productId);
            }
        }
    }
}
