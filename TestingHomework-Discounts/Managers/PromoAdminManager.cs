using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingHomework_Discounts.Managers
{
    public class PromoAdminManager
    {

        public IEnumerable<PromoCode> GetAllPromos()
        {
            using (PromoRepository db = new PromoRepository())
            {
                return db.PromoCodes.ToList();
            }
        }

        public PromoCode SavePromo(PromoCode promo)
        {
            using (PromoRepository db = new PromoRepository())
            {
                if (promo.Id == Guid.Empty)
                {
                    db.PromoCodes.Add(promo);
                }
                else
                {
                    db.PromoCodes.Attach(promo);
                    db.Entry(promo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                db.SaveChanges();
            }

            return promo;
        }
    }
}
