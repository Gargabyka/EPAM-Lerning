using Ado.Net.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.Net.EF.Interfaces
{
    public interface IApplicationConfig
    {
        List<Categories> GetCategories();

        List<CustomerDemographics> GetCustomerDemographics();

        List<Customers> GetCustomers();

        List<Employees> GetEmployees();

        List<Order_Details> GetOrderDetails();

        List<Orders> GetOrders();

        List<Products> GetProducts();

        List<Region> GetRegion();

        List<Shippers> GetShippers();

        List<Suppliers> GetSuppliers();

        List<Territories> GetTerritories();
    }
}
