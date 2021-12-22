using EFCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Interfaces
{
    public interface IService
    {
        List<Orders> GetOrders();

        List<Categories> GetProducts();
    }
}
