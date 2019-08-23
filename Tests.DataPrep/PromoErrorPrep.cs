//using Shared.DatabaseContext.DBOs;
//using Shared.DataContracts;
using Shared.DatabaseContext.DBOs;
using Shared.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tests.DataPrep
{
    public class PromoErrorPrep : TypePrepBase<PromoError, PromoErrorDTO>
    {

        public PromoErrorPrep(ITestDataAccessor dataAccessor) : base (dataAccessor)
        {
        }

        public PromoError CreateData(bool? isComplete = null, bool isPersisted = true)
        {
           PromoError promoCode = new PromoError()
            {
                ErrorCode = PromoErrorType.Unknown,
                Message = random.Lorem.Text()               
            };

            if (isPersisted)
            {
                PromoError savedItem = Create(promoCode);
                return savedItem;
            }
            else
            {
                return promoCode;
            }
        }
        
        public IEnumerable<PromoError> CreateManyForList(int count)
        {
            List<PromoError> itemList = new List<PromoError>();
            for (int i = 0; i < count; i++)
            {
                PromoError item = CreateData();
                itemList.Add(item);                
            }
            return itemList;
        }
    }
}
