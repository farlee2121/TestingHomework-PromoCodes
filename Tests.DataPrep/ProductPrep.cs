
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingHomework_Discounts;

namespace Tests.DataPrep
{

    public class ProductPrep : TypePrepBase<Product>
    {
        public ProductPrep(ITestDataAccessor dataAccessor) : base(dataAccessor)
        {
        }
    }
}
