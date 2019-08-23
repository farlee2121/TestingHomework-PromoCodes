using Accessors.DatabaseAccessors;
using Shared.DataContracts;
using System;
using System.Collections.Generic;

namespace Managers.LazyCollectionOfAllManagers
{
    public interface IUserAdminManager
    {
        User GetUser(Guid userId);
        IEnumerable<User> GetAllUsers();
        SaveResult<User> SaveUser(User user);
    }

    public class UserAdminManager
    {
        IUserAccessor userAccessor;
        public UserAdminManager(IUserAccessor userAccessor)
        {
            this.userAccessor = userAccessor;
        }

        public User GetUser(Guid userId)
        {
            return userAccessor.GetUser(userId);
        }
        public IEnumerable<User> GetAllUsers()
        {
            return userAccessor.GetAllUsers();
        }

        public SaveResult<User> SaveUser(User user)
        {
            return userAccessor.SaveUser(user);
        }
    }
}
