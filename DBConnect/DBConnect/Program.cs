using System;
using System.Linq;
using Ado.Net.Dal.Implementations;
using Ado.Net.Dal.Interfaces;
using Ado.Net.EF;
using Ado.Net.EF.Interfaces;
using Ado.Net.EF.Models;
using Ado.Net.Logger;
using Ado.Net.Logger.Interfaces;
using Unity;

namespace DBConnect
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<INorthwindDal, NorthwindDal>();
            container.RegisterType<IServiceLog, ServiceLog>();
            container.RegisterType<IApplicationConfig, ApplicationConfig>();

            var northwind = container.Resolve<INorthwindDal>(); 
            var applicationConfig = container.Resolve<IApplicationConfig>();

            var categories = applicationConfig.GetCategories();

            foreach (var cate in categories)
            {
                Console.WriteLine($"Id = {cate.CategoryID} Category = {cate.CategoryName}");
                foreach (var p in cate.Products)
                {
                    Console.WriteLine($"SuppliersName = {p.Suppliers.CompanyName} ProductName = {p.ProductName}");
                }
            }

            Console.ReadKey();


            //var list = northwindDal.GetOrders();
            //var list = northwindDal.CustOrdersDetail(10250);
            // northwindDal.AddRow(5, "Avrora", "Moscow");
        }
    }
}
