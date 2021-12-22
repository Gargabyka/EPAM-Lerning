using Ado.Net.Dal.Implementations;
using Ado.Net.Dal.Interfaces;
using Ado.Net.Unity;
using Unity;

namespace DBConnect
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<INorthwindDal, NorthwindDal>();

            var northwind = container.Resolve<INorthwindDal>();

            var list = northwind.CustOrdersDetail(10250);

            //var list = northwindDal.GetOrders();
            //var list = northwindDal.CustOrdersDetail(10250);
            // northwindDal.AddRow(5, "Avrora", "Moscow");
        }
    }
}
