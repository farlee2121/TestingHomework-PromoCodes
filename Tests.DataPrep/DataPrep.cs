
namespace Tests.DataPrep
{
    public class ProductDataPrep
    {
        ITestDataAccessor dataPersistance;       

        public ProductPrep Product { get; set; }
        public ProductListPrep ProductLists { get; set; }
        public ProductItemPrep ProductItems { get; set; }

        public ProductDataPrep(bool shouldPersistData)
        {
            if (shouldPersistData)
            {
                dataPersistance = new ApplicationDbTestDataAccessor();
            }
            else
            {
                dataPersistance = new NoPersistanceTestDataAccessor();
            }

            Product = new ProductPrep(dataPersistance);
            ProductLists = new ProductListPrep(dataPersistance, Product);
            ProductItems = new ProductItemPrep(dataPersistance, ProductLists);
        }

        public void EnsureDatastore()
        {
            dataPersistance.EnsureDatastore();
        }
    }
}
