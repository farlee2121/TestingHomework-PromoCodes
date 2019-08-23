namespace Tests.DataPrep
{
    public class DiscountsDataPrep
    {
        ITestDataAccessor dataPersistance;       

        public ProductPrep Products { get; set; }
        public PromoCodePrep Promocodes { get; set; }
        public UserPrep Users { get; set; }
        public PromoErrorPrep PromoErrors { get; set; }
        public CartPrep Carts { get; set; }
        public CartProductPrep CartProducts { get; set; }
        public CartPromoPrep CartPromos { get; set; }

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

            Products = new ProductPrep(dataPersistance);
            Promocodes = new PromoCodePrep(dataPersistance, Products);           
            Users = new UserPrep(dataPersistance);
            PromoErrors = new PromoErrorPrep(dataPersistance);
            Carts = new CartPrep(dataPersistance, Products, Users, Promocodes, PromoErrors);
            CartProducts = new CartProductPrep(dataPersistance, Products, Carts);
            CartPromos = new CartPromoPrep(dataPersistance, Promocodes, Carts);
        }

        public void EnsureDatastore()
        {
            dataPersistance.EnsureDatastore();
        }
    }
}
