using Shared.DatabaseContext;
using Shared.DatabaseContext.DBOs;
using Shared.DataContracts;
using System.Collections.Generic;
using System.Linq;

namespace Accessors.DatabaseAccessors
{
    public interface IPromoAccessor
    {       
        IEnumerable<PromoCode> GetAllPromos(Id productId);
        SaveResult<PromoCode> SavePromo(PromoCode promo);

    }
    public class PromoAccessor : IPromoAccessor
    {
        PromoCode_Mapper mapper = new PromoCode_Mapper();

        public IEnumerable<PromoCode> GetAllPromos(Id productId)
        {
            IEnumerable<PromoCode> promoCodeList;
            using (TestingHomeworkContext db = new TestingHomeworkContext())
            {
                IEnumerable<PromoCodeDTO> promoCodeModellList = db.PromoCodes.Where(pc => pc.ProductId == productId & pc.IsActive).ToList();
                promoCodeList = mapper.ModelListToContractList(promoCodeModellList);
            }
            return promoCodeList;
        }

        public SaveResult<PromoCode> SavePromo(PromoCode promo)
        {
            using (TestingHomeworkContext db = new TestingHomeworkContext())
            {
                PromoCodeDTO dbModel = mapper.ContractToModel(promo);

                db.AddOrUpdate(dbModel);
                db.SaveChanges();

                PromoCode savedProduct = mapper.ModelToContract(dbModel);

                SaveResult<PromoCode> saveResult = new SaveResult<PromoCode>(savedProduct);
                return saveResult;
            }
        }

    }
}
