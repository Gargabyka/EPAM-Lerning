using Ado.Net.Dal.Implementations;
using Ado.Net.Dal.Interfaces;
using Ado.Net.Logger;
using Ado.Net.Logger.Interfaces;
using Unity;

namespace Ado.Net.Unity
{
    public class Container
    {
        public Container()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IServiceLog, ServiceLog>();
            container.RegisterType<INorthwindDal, NorthwindDal>();
        }
    }
}