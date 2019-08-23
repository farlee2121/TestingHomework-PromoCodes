//using Shared.DatabaseContext.DBOs;
//using Shared.DataContracts;
using Shared.DatabaseContext.DBOs;
using Shared.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tests.DataPrep
{
    public class UserPrep : TypePrepBase<User, UserDTO>
    {
        //CartPrep cartPrep;

        //public UserPrep(ITestDataAccessor dataAccessor, CartPrep cartPrep) : base(dataAccessor)
        //{
        //    this.cartPrep = cartPrep;
        //}

        public UserPrep(ITestDataAccessor dataAccessor) : base(dataAccessor)
        {

        }

        public User CreateData(IEnumerable<Cart> carts = null,bool isPersisted = true)
        {
            IEnumerable<Cart> sanitizedCart = carts; //?? cartPrep.CreateManyForList(1);
         
            User user = new User()
            {
                FirstName = random.Person.FirstName,
                LastName = random.Person.LastName,                
                //Carts = sanitizedCart,
            };

            if (isPersisted)
            {
                User savedItem = Create(user);
                return savedItem;
            }
            else
            {
                return user;
            }
        }


        public IEnumerable<User> CreateManyForList(int count, IEnumerable<Cart> carts = null)
        {
            List<User> itemList = new List<User>();
            for (int i = 0; i < count; i++)
            {
                User item = CreateData(carts);
                itemList.Add(item);                
            }
            return itemList;
        }
    }
}
