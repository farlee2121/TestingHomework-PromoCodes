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
    public class PromoCodePrep : TypePrepBase<PromoCode, PromoCodeDTO>
    {
       ProductPrep ProductPrep;

        public PromoCodePrep(ITestDataAccessor dataAccessor, ProductPrep ProductPrep) : base (dataAccessor)
        {
            this.ProductPrep = ProductPrep;
        }

        public PromoCode CreateData(Product product = null, bool isPersisted = true)
        {
            Product sanitizedProduct = product ?? ProductPrep.Create();

            var date = Convert.ToDateTime(random.Date.Future());
            PromoCode promoCode = new PromoCode()
            {
                Code = random.Lorem.Word(),
                DollarDiscount = Convert.ToDouble(random.PickRandom(100,200)),
                StartDate = date,
                EndDate = date.AddMonths(1),
                MaxRedemptionCount = random.Random.Number(),
                RedemptionCount = random.Random.Number(),
                ProductId = sanitizedProduct.Id,
                //Product= sanitizedProduct,
            };

            if (isPersisted)
            {
                PromoCode savedItem = Create(promoCode);
                return savedItem;
            }
            else
            {
                return promoCode;
            }
        }


        public IEnumerable<PromoCode> CreateManyForList(int count, Product product, bool isPersisted = true)
        {
            List<PromoCode> itemList = new List<PromoCode>();
            for (int i = 0; i < count; i++)
            {
                PromoCode item = CreateData(product,isPersisted);
                itemList.Add(item);                
            }
            return itemList;
        }
    }
}
