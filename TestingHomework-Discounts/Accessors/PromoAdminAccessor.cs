using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestingHomework.Tests.Data;

namespace TestingHomework_Discounts.Accessors
{
    public interface IPromoAdminAccessor

    {
        IEnumerable<PromoCode> GetAllPromos();
        PromoCode SavePromo(PromoCode promo);

    }
    class PromoAdminAccessor : IPromoAdminAccessor
    {
        PromoCode_Mapper mapper = new PromoCode_Mapper();

        public IEnumerable<PromoCode> GetAllPromos()
        {
            using (PromoRepository db = new PromoRepository())
            {
              var promocodeList = db.PromoCodes.ToList();
                return mapper.ModelListToContractList(promocodeList);
            }

        

        }
        public PromoCode SavePromo(PromoCode promo)
        {
            using (PromoRepository db = new PromoRepository())
            {
                PromoCodeDTO dbModel = mapper.ContractToModel(promo);
                db.AddOrUpdate(dbModel);
                db.SaveChanges();
                PromoCode savedPromocode = mapper.ModelToContract(dbModel);
                return savedPromocode;
            }
         
        }

    }
}
