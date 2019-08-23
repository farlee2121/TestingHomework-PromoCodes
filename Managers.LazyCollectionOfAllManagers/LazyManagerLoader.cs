using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers.LazyCollectionOfAllManagers
{
    public class LazyManagerLoader : NinjectModule
    {
        public override void Load()
        {           
            Bind<IProductAdminManager>().To<IProductAdminManager>();
            Bind<IPromoAdminManager>().To<IPromoAdminManager>();
            Bind<IUserAdminManager>().To<IUserAdminManager>();
            Bind<ICheckoutManager>().To<ICheckoutManager>();
        }
    }
}
