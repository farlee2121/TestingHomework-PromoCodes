using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestingHomework.Tests.Data;

namespace TestingHomework_Discounts.Accessors
{
    public interface IUserAdminAccessor
    {
        User GetUser(Guid userId);
        User SaveUser(User user);

        IEnumerable<User> GetAllUsers();
    }
    public class UserAdminAccessor: IUserAdminAccessor
    {
        User_Mapper mapper = new User_Mapper();
        public User GetUser(Guid userId)
        {
            using (PromoRepository db = new PromoRepository())
            {
                var user = db.Users.FirstOrDefault(_user => _user.Id == userId);
                var expectedUser = mapper.ModelToContract(user);
                return expectedUser;
            }            
            
        }

        public User SaveUser(User user)
        {
            using (PromoRepository db = new PromoRepository())
            {
                UserDTO dbModel = mapper.ContractToModel(user);
                db.AddOrUpdate(dbModel);
                db.SaveChanges();
                User savedUser = mapper.ModelToContract(dbModel);
                return savedUser;
            }
        }
        

        public IEnumerable<User> GetAllUsers()
        {
            using (PromoRepository db = new PromoRepository())
            {
               var userList= db.Users.ToList();                

                return mapper.ModelListToContractList(userList);
            }
        }
    }
}
