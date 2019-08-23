using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accessors.DatabaseAccessors
{
    public class DatabaseAccessorLoader : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductAccessor>().To<ProductAccessor>();
            Bind<IPromoAccessor>().To<PromoAccessor>();
            Bind<IUserAccessor>().To<UserAccessor>();
            Bind<ICheckoutAccessor>().To<CheckoutAccessor>();
        }
    }
}
