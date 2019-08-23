using Accessors.DatabaseAccessors;
using Shared.DataContracts;
using System.Collections.Generic;

namespace Managers.LazyCollectionOfAllManagers
{

    public interface IPromoAdminManager
    {
        IEnumerable<PromoCode> GetAllPromos(Id productId);

        SaveResult<PromoCode> SavePromo(PromoCode promoCode);
    }

    public class PromoAdminManager : IPromoAdminManager
    {
        IPromoAccessor promoAccessor;
        IProductAccessor productAccessor;
        public PromoAdminManager(IPromoAccessor promoAccessor, IProductAccessor productAccessor)
        {
            this.promoAccessor = promoAccessor;
            this.productAccessor = productAccessor;
        }

        public IEnumerable<PromoCode> GetAllPromos(Id productId)
        {
            return promoAccessor.GetAllPromos(productId);
        }

        public SaveResult<PromoCode> SavePromo(PromoCode promoCode)
        {
            return promoAccessor.SavePromo(promoCode);
        }
    }
}
