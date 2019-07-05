using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingHomework_Discounts.Managers
{
    public class UserAdminManager
    {

        public User GetUser(Guid userId)
        {
            using (PromoRepository db = new PromoRepository())
            {
                return db.Users.FirstOrDefault(_user => _user.Id == userId);
            }
        }

        public User SaveUser(User user)
        {
            using (PromoRepository db = new PromoRepository())
            {
                if (user.Id == Guid.Empty)
                {
                    db.Users.Add(user);
                }
                else
                {
                    db.Users.Attach(user);
                    db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                db.SaveChanges();
            }

            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            using (PromoRepository db = new PromoRepository())
            {
                return db.Users.ToList();
            }
        }
    }
}
