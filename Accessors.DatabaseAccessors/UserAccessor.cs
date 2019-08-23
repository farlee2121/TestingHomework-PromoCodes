using Microsoft.EntityFrameworkCore;
using Shared.DatabaseContext;
using Shared.DatabaseContext.DBOs;
using Shared.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Accessors.DatabaseAccessors
{
    public interface IUserAccessor
    {
        User GetUser(Guid userId);

        IEnumerable<User> GetAllUsers();

        SaveResult<User> SaveUser(User user);
    }
    public class UserAccessor : IUserAccessor
    {
       User_Mapper mapper = new User_Mapper();

        public User GetUser(Guid userId)
        {
            User user;
            using (TestingHomeworkContext db = new TestingHomeworkContext())
            {
                UserDTO userModel = db.Users.Where(ti => ti.Id == userId && ti.IsActive).FirstOrDefault();
                user = mapper.ModelToContract(userModel);
            }
            return user;
        }


        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> userList;
            using (TestingHomeworkContext db = new TestingHomeworkContext())
            {
                IEnumerable<UserDTO> userModellList = db.Users.Where(pc => pc.IsActive).ToList();
                //foreach (UserDTO user in userModellList)
                //{
                //    user.Carts= db.Carts.Where(c => c.UserId== user.Id).ToList();//.Include(_cart => _cart.) 
                //}
                userList = mapper.ModelListToContractList(userModellList);
            }
            return userList;
        }

        public SaveResult<User> SaveUser(User user)
        {
            using (TestingHomeworkContext db = new TestingHomeworkContext())
            {
                UserDTO dbModel = mapper.ContractToModel(user);

                db.AddOrUpdate(dbModel);
                db.SaveChanges();

                User savedUser = mapper.ModelToContract(dbModel);

                SaveResult<User> saveResult = new SaveResult<User>(savedUser);
                return saveResult;
            }
        }

    }
}
