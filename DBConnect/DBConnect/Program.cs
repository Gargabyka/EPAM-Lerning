using System;
using System.Linq;
using Ado.Net.Dal.Implementations;
using Ado.Net.Dal.Interfaces;
using Ado.Net.EntityFramework;
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

            var context = new UserDbContext();
            var products = context.Categories.ToList();
            
            foreach (var prod in products)
            {
                Console.WriteLine($"Id = {prod.CategoryId} Category = {prod.CategoryName}");
                foreach (var p in prod.Products)
                {
                    Console.WriteLine($"SuppliersName = {p.Suppliers.CompanyName} ProductName = {p.ProductName}");
                }
            }

            //var list = northwindDal.GetOrders();
            //var list = northwindDal.CustOrdersDetail(10250);
            // northwindDal.AddRow(5, "Avrora", "Moscow");
        }
    }
}
