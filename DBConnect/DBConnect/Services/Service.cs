using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Ado.Net.Entities;
using Ado.Net.Enum;
using DBConnect.DTO;

namespace DBConnect
{
    /// <summary>
    /// Сервис для работы с SQL запросами
    /// </summary>
    public class Service
    {
        private readonly SqlConnection _connection;
        private readonly ServiceLog _log;
        public Service(SqlConnection connection, ServiceLog log)
        {
            _connection = connection;
            _log = log;
        }
        /// <summary>
        /// Показывать список введенных заказов (таблица [Orders]). Помимо основных полей должны возвращаться:
        /// a. Статус заказа в виде Enum поля;
        /// </summary>
        public void GetOrders()
        {
            List<Orders> ordersList = new List<Orders>();
            SqlCommand command = new SqlCommand(SqlQuery.GetOrders, _connection);
            try
            {
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        var order = new Orders();
                        order.OrderId = (int) reader["OrderId"];
                        order.ShipName = (string) reader["ShipName"];
                        order.ShipAddress = (string) reader["ShipAddress"];         
                        order.ShipCity = (string) reader["ShipCity"];         
                        order.ShipCountry = (string) reader["ShipCountry"];
                        order.OrderDate = reader["OrderDate"].ToString();
                        order.ShippedDate = reader["ShippedDate"].ToString();
                    
                        if (string.IsNullOrEmpty(order.OrderDate))
                        {
                            order.StateOrder = StateOrder.New;
                        }

                        order.StateOrder = string.IsNullOrEmpty(order.ShippedDate)
                            ? order.StateOrder = StateOrder.AtWork 
                            : order.StateOrder = StateOrder.Completed;
          
                        ordersList.Add(order);
                    }
                }
                
                foreach (var order in ordersList)
                {
                    Console.WriteLine($"Id = {order.OrderId} ShipName = {order.ShipName} State = {order.StateOrder}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _log.AddRowLog(e);
            }
        }

        /// <summary>
        /// Показывать подробные сведения о конкретном заказе, включая номенклатуру заказа
        /// </summary>
        public void GetFullInformationOrders(int orderID)
        {
            List<InformationOrder> ordersList = new List<InformationOrder>();
            SqlCommand command = new SqlCommand(SqlQuery.GetFullInformationOrders, _connection);
            try
            {
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        var order = new InformationOrder();
                        order.OrderId = (int) reader["OrderId"];
                        order.ProductId = (int) reader["ProductID"];
                        order.UnitPrice = (decimal) reader["UnitPrice"];
                        order.Quantity = (Int16) reader["Quantity"];
                        order.ProductName = (string) reader["ProductName"];
                        order.SupplierID = (int) reader["SupplierID"];
                        order.CategoryID = (int) reader["CategoryID"];
                        order.QuantityPerUnit = (string) reader["QuantityPerUnit"];
                        order.UnitsInStock = (Int16) reader["UnitsInStock"];
                        order.UnitsOnOrder = (Int16) reader["UnitsOnOrder"];
                        order.ReorderLevel = (Int16) reader["ReorderLevel"];
                        ordersList.Add(order);
                    }
                }
                
                foreach (var order in ordersList)
                {
                    if (order.OrderId == orderID)
                    {
                        Console.WriteLine(
                            $"Id = {order.OrderId} ProductId = {order.ProductId} UnitPrice = {order.UnitPrice}" +
                            $"Quantity = {order.Quantity}  ProductName = {order.ProductName}" +
                            $"SupplierID = {order.SupplierID} CategoryID = {order.CategoryID} QuantityPerUnit = {order.QuantityPerUnit}" +
                            $"UnitsInStock = {order.UnitsInStock} UnitsOnOrder = {order.UnitsOnOrder} ReorderLevel = {order.ReorderLevel}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _log.AddRowLog(e);
            }
        }

        /// <summary>
        /// Добавить запись в таблицу
        /// </summary>
        public void AddRow(int employeeId, string shipName, string shipCity)
        {
            SqlCommand command = new SqlCommand(SqlQuery.AddRow, _connection);
            try
            {
                SqlParameter param = new SqlParameter()
                {
                    ParameterName = "@EmployeeID",
                    Value = employeeId,
                    SqlDbType = SqlDbType.Int
                };
                command.Parameters.Add(param);

                param = new SqlParameter()
                {
                    ParameterName = "@ShipName",
                    Value = shipName,
                    SqlDbType = SqlDbType.NVarChar,
                };
                command.Parameters.Add(param);

                param = new SqlParameter()
                {
                    ParameterName = "@ShipCity",
                    Value = shipCity,
                    SqlDbType = SqlDbType.NVarChar,
                };
                command.Parameters.Add(param);
                
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _log.AddRowLog(e);
            }
        }

        /// <summary>
        /// Удалить заказы со статусом "Новый" и "В работе"
        /// </summary>
        public void DeleteOrders()
        {
            SqlCommand command = new SqlCommand(SqlQuery.DeleteOrders, _connection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                _log.AddRowLog(e);
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Вызов процедуры "CustOrderHist"
        /// </summary>
        /// <param name="customerID">Id клиента</param>
        public void CustOrderHist(string customerID)
        {
            List<CustOrderHist> custList = new List<CustOrderHist>();
            SqlCommand command = new SqlCommand(SqlQuery.CustOrderHist, _connection);
            try
            {
                SqlParameter param = new SqlParameter()
                {
                    ParameterName = "@CustomerID",
                    Value = customerID,
                    SqlDbType = SqlDbType.NVarChar
                };
                command.Parameters.Add(param);
                
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        var cust = new CustOrderHist();
                        cust.ProductName = (string) reader["ProductName"];
                        cust.Total = (int) reader["Total"];

                        custList.Add(cust);
                    }
                }
                
                foreach (var cus in custList)
                {
                    Console.WriteLine($"Product Name = {cus.ProductName} Total = {cus.Total}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _log.AddRowLog(e);
            }
        }

        public void CustOrdersDetail(int orderId)
        {
            List<CustOrdersDetail> custList = new List<CustOrdersDetail>();
            SqlCommand command = new SqlCommand(SqlQuery.CustOrdersDetail, _connection);
            try
            {
                SqlParameter param = new SqlParameter()
                {
                    ParameterName = "@OrderID",
                    Value = orderId,
                    SqlDbType = SqlDbType.Int
                };
                command.Parameters.Add(param);
                
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        var cust = new CustOrdersDetail();
                        cust.ProductName = (string) reader["ProductName"];
                        cust.UnitPrice = (decimal) reader["UnitPrice"];
                        cust.Quantity = (Int16) reader["Quantity"];
                        cust.Discount = (int) reader["Discount"];
                        cust.ExtendedPrice = (decimal) reader["ExtendedPrice"];

                        custList.Add(cust);
                    }
                }
                
                foreach (var cus in custList)
                {
                    Console.WriteLine($"{cus.ProductName}\t {cus.UnitPrice}\t {cus.Quantity}\t {cus.Discount}\t {cus.ExtendedPrice}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _log.AddRowLog(e);
            }
        }
    }
}