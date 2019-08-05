using System;
using System.Collections.Generic;
using System.Text;
using TestingHomework.Tests.Data;
using TestingHomework_Discounts;

namespace TestingHomework.Tests.DataPrep
{
      public class CartsPrep : TypePrepBase<Cart, Data.CartDTO>
    {
        public CartsPrep(ITestDataAccessor dataAccessor) : base(dataAccessor)
        {

        }

        public IEnumerable<Cart> CreateManyForCarts(int count, Cart cart)
        {
            List<Cart> carts = new List<Cart>();
            for (int i = 0; i < count; i++)
            {
                Cart cartList = Create(cart);
                carts.Add(cartList);
            }

            return carts;
        }
        public override Cart Create(Cart cart = null, bool isPersisted = true)
        {
            Cart sanitizedCart = cart ?? Create();
            List<Product> cartProducts = new List<Product>
            {
                new Product()
                {
                    Price = Faker.RandomNumber.Next(),
                    Description = Faker.Lorem.Paragraph()
                },

                new Product()
                {
                    Price = Faker.RandomNumber.Next(),
                    Description = Faker.Lorem.Paragraph()
                }
            };

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

            Cart cartItem = new Cart()
            {
                User = cartUser,
                Products = cartProducts,
                PromoCodes = cartPromos,
                Id = sanitizedCart.Id,
            };

         

            if (isPersisted)
            {
                Cart savedCart = base.Create(cartItem);
                return savedCart;
            }
            else
            {
                return cartItem;
            }

        }
    }
}