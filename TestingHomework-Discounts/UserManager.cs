﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingHomework_Discounts
{
    class UserManager
    {

        public User GetUser(Id userId)
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
                if (user.Id.IsDefault())
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
    }
}
