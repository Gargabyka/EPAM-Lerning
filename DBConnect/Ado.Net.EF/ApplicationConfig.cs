using Ado.Net.EF.Interfaces;
using Ado.Net.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.Net.EF
{
    public class ApplicationConfig: IApplicationConfig
    {
        private readonly Model _model;
        public ApplicationConfig()
        {
            _model = new Model();
        }

        public List<Categories> GetCategories()
        {
            var list = _model.Categories.ToList();

            return list;
        }

        public List<CustomerDemographics> GetCustomerDemographics()
        {
            var list = _model.CustomerDemographics.ToList();

            return list;
        }

        public List<Customers> GetCustomers()
        {
            var list = _model.Customers.ToList();

            return list;
        }

        public List<Employees> GetEmployees()
        {
            var list = _model.Employees.ToList();

            return list;
        }

        public List<Order_Details> GetOrderDetails()
        {
            var list = _model.Order_Details.ToList();

            return list;
        }

        public List<Orders> GetOrders()
        {
            var list = _model.Orders.ToList();

            return list;
        }

        public List<Products> GetProducts()
        {
            var list = _model.Products.ToList();

            return list;
        }

        public List<Region> GetRegion()
        {
            var list = _model.Region.ToList();

            return list;
        }

        public List<Shippers> GetShippers()
        {
            var list = _model.Shippers.ToList();

            return list;
        }

        public List<Suppliers> GetSuppliers()
        {
            var list = _model.Suppliers.ToList();

            return list;
        }

        public List<Territories> GetTerritories()
        {
            var list = _model.Territories.ToList();

            return list;
        }
    }
}
