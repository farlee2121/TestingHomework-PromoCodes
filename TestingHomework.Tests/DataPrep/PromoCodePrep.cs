using System;
using System.Collections.Generic;
using System.Text;
using TestingHomework.Tests.Data;
using TestingHomework_Discounts;

namespace TestingHomework.Tests.DataPrep
{
       public class PromoCodePrep : TypePrepBase<PromoCode, Data.PromoCodeDTO>
    {
        public PromoCodePrep(ITestDataAccessor dataAccessor) : base(dataAccessor)
        {

        }

        public IEnumerable<PromoCode> CreateManyForPromocodes(int count, PromoCode promoCode)
        {
            List<PromoCode> promoCodeList = new List<PromoCode>();
            for (int i = 0; i < count; i++)
            {
                PromoCode productList = Create(promoCode);
                promoCodeList.Add(promoCode);
            }

            return promoCodeList;
        }
        public override PromoCode Create(PromoCode promoCode = null, bool isPersisted = true)
        {
            PromoCode sanitizedPromoCode = promoCode ?? Create();
            PromoCode promocodeItem = new PromoCode()
            {
                Id = sanitizedPromoCode.Id,
                Product = new Product {
                    Description = Faker.Lorem.Sentence(),
                    Price = Faker.RandomNumber.Next(5),
                    Id = sanitizedPromoCode.Id
                },
                DollarDiscount = 0.0,
                EndDate = null,
                StartDate = Faker.DateTimeFaker.DateTime().AddDays(50),
                Code = "20ff",
                MaxRedemptionCount = 50,
                RedemptionCount = 0               
               
            };

            if (isPersisted)
            {
                PromoCode savedPromoCode = base.Create(promocodeItem);
                return savedPromoCode;
            }
            else
            {
                return promocodeItem;
            }

        }
    }
}