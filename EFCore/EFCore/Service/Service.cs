using EFCore.Interfaces;
using EFCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore
{
    class Service : IService
    {
        private NorthwindEntities context;
            
        public Service()
        {
            context = new NorthwindEntities();
        }
        public List<Orders> GetOrders()
        {
            var list = context.Orders.ToList();

            return list;
        }

        public List<Categories> GetProducts()
        {
            var list = context.Categories.ToList();

            return list;
        }
    }
}
