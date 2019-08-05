using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestingHomework_Discounts.Accessors;

namespace TestingHomework_Discounts.Managers
{
    public interface IPromoAdminManager
    {
        IEnumerable<PromoCode> GetAllPromos();
        PromoCode SavePromo(PromoCode promo);       

    }
    public class PromoAdminManager: IPromoAdminManager
    {

        IPromoAdminAccessor promoAdminAccessor;
        public PromoAdminManager(IPromoAdminAccessor promoAdminAccessor)
        {
            this.promoAdminAccessor = promoAdminAccessor;
        }

        public IEnumerable<PromoCode> GetAllPromos()
        {
            
                return promoAdminAccessor.GetAllPromos();
           
        }

        public PromoCode SavePromo(PromoCode promo)
        {
            return promoAdminAccessor.SavePromo(promo);
        }
    }
}
