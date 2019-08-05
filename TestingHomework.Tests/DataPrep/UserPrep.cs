using System;
using System.Collections.Generic;
using System.Text;
using TestingHomework.Tests.Data;
using TestingHomework_Discounts;

namespace TestingHomework.Tests.DataPrep
{
    public class UserPrep : TypePrepBase<User, UserDTO>
    {
        public UserPrep(ITestDataAccessor dataAccessor) : base(dataAccessor)
        {
        }
        public IEnumerable<User> CreateManyForUsers(int count, User user)
        {
            List<User> users = new List<User>();
            for (int i = 0; i < count; i++)
            {
                User userList = Create(user);
                users.Add(userList);
            }

            return users;
        }
        public override User Create(User user = null, bool isPersisted = true)
        {
            User sanitizeduser = user ?? Create();
      
          
            
            List<Product> cartProducts = new List<Product>();
            cartProducts.Add(new Product()
            {
                Price = Faker.RandomNumber.Next(),
                Description = Faker.Lorem.Paragraph()
            });

            cartProducts.Add(new Product()
            {
                Price = Faker.RandomNumber.Next(),
                Description = Faker.Lorem.Paragraph()
            });

            List<PromoCode> cartPromos = new List<PromoCode>
            {
                new PromoCode()
                {
                    Code = Faker.StringFaker.AlphaNumeric(5),
                    DollarDiscount = Faker.NumberFaker.Number(10),
                }
            };
            User cartUser = new User()
            {
                FirstName = Faker.Name.First(),
                LastName = Faker.Name.Last()
            };

            Cart expectedCart = new Cart()
            {
                User = cartUser,
                Products = cartProducts,
                PromoCodes = cartPromos,
            };
           

            User userItem = new User()
            {
                Id = sanitizeduser.Id,
                FirstName = expectedCart.User.FirstName,
                LastName = expectedCart.User.LastName,
                Carts= null,
            };

            if (isPersisted)
            {
                User saveduser = base.Create(userItem);
                return saveduser;
            }
            else
            {
                return userItem;
            }

        }
    }
}
