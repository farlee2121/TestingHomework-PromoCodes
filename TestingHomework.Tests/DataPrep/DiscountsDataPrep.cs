using System;
using System.Collections.Generic;
using System.Text;
using TestingHomework_Discounts;

namespace TestingHomework.Tests.DataPrep
{
    public class DiscountsDataPrep
    {
        ITestDataAccessor dataPersistance;
        
        public PromoCodePrep PromoCodes { get; set; }
        public ProductPrep Products { get; set; }
        public UserPrep Users { get; set; }
        public CartsPrep Carts { get; set; }
        public CartProduct CartProducts { get; set; }
        public CartPromo CartPromos { get; set; }


        public DiscountsDataPrep(bool shouldPersistData)
        {
            if (shouldPersistData)
            {
                dataPersistance = new ApplicationDbTestDataAccessor();
            }
            else
            {
                dataPersistance = new NoPersistanceTestDataAccessor();
            }

             Users = new UserPrep(dataPersistance);
             PromoCodes = new PromoCodePrep(dataPersistance);
              Products = new ProductPrep(dataPersistance);
             Carts = new CartsPrep(dataPersistance);
        }

        public void EnsureDatastore()
        {
            dataPersistance.EnsureDatastore();
        }
    }
}
