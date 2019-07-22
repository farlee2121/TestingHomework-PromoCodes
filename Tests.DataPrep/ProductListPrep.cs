
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingHomework_Discounts;

namespace Tests.DataPrep
{
    public class ProductListPrep : TypePrepBase<Product>
    {
        ProductPrep productPrep;
        public ProductListPrep(ITestDataAccessor dataAccessor, ProductPrep productPrep) : base(dataAccessor)
        {
            this.productPrep = productPrep;
        }

        public Product CreateData(Product product = null, bool isPersisted = true)
        {
            Product sanitizedProduct = product ?? productPrep.Create();
            Product products = new Product()
            {
                //Id = sanitizedProduct.Id,
                Description = random.Lorem.Sentence(),
                Price = sanitizedProduct.Price
            };

            if (isPersisted)
            {
                Product savedList = base.Create(products);
                return savedList;
            }
            else
            {
                return products;
            }
        }

        //public IEnumerable<TodoList> CreateManyForUser(int count, User user)
        //{
        //    List<TodoList> todoLists = new List<TodoList>();
        //    for (int i = 0; i < count; i++)
        //    {
        //        TodoList todoList = Create(user);
        //        todoLists.Add(todoList);
        //    }

        //    return todoLists;
        //}
    }
}
