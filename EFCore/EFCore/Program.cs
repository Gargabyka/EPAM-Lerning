using EFCore.Interfaces;
using System;
using Unity;

namespace EFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IService, Service>();
            var service = container.Resolve<IService>();

            var products = service.GetProducts();

            foreach (var prod in products)
            {
                Console.WriteLine($"Id = {prod.CategoryID} Category = {prod.CategoryName}");
                foreach (var p in prod.Products)
                {
                    Console.WriteLine($"SuppliersName = {p.Suppliers.CompanyName} ProductName = {p.ProductName}");
                }
            }

            Console.ReadKey();
        }
    }
}
