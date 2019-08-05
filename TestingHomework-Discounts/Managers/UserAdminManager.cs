using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestingHomework_Discounts.Accessors;

namespace TestingHomework_Discounts.Managers
{
    public interface IUserAdminManager
    {
        User GetUser(Guid userId);
        User SaveUser(User user);
        IEnumerable<User> GetAllUsers();

    }
    public class UserAdminManager : IUserAdminManager
    {

        IUserAdminAccessor userAdminAccessor;
        public UserAdminManager(IUserAdminAccessor userAdminAccessor)
        {
            this.userAdminAccessor = userAdminAccessor;
        }
    

        public User GetUser(Guid userId)
        {
          
                return userAdminAccessor.GetUser(userId);
           
        }

        public User SaveUser(User user)
        {
          
            return userAdminAccessor.SaveUser(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return userAdminAccessor.GetAllUsers();
        }
    }
}
