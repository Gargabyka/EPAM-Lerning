using Unity;
using WebApiConsoleApplication.Interfaces;

namespace WebApiConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IOrderService, OrderService>();
            
            var orders = container.Resolve<IOrderService>();
            
            //orders.GetOrder(11084);
            //var delete = orders.DeleteOrder(11084);
            var post = orders.PostOrder("Судно", "Город", "Страна");
        }
    }
}