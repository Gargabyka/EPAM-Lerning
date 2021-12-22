using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Ado.Net.Contracts.Entities;
using Ado.Net.Contracts.Enums;
using Ado.Net.Dal.Interfaces;
using Ado.Net.Logger;
using Ado.Net.Logger.Interfaces;
using Ado.Net.SqlHelper;
using Unity;

namespace Ado.Net.Dal.Implementations
{
    public class NorthwindDal : INorthwindDal
    {
        private readonly string _connectionString;
        private IServiceLog _log;

        public NorthwindDal()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            IUnityContainer container = new UnityContainer();
            container.RegisterType<IServiceLog, ServiceLog>();
            _log = container.Resolve<IServiceLog>();
        }

        /// <summary>
        /// Показывать список введенных заказов (таблица [Orders]). Помимо основных полей должны возвращаться:
        /// a. Статус заказа в виде Enum поля;
        /// </summary>
        public List<Orders> GetOrders()
        {
            List<Orders> ordersList = new List<Orders>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand(SqlQuery.GetOrders, connection);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            var order = new Orders();
                            order.OrderId = (int)reader["OrderId"];
                            order.ShipName = (string)reader["ShipName"];
                            order.ShipAddress = (string)reader["ShipAddress"];
                            order.ShipCity = (string)reader["ShipCity"];
                            order.ShipCountry = (string)reader["ShipCountry"];
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
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                _log.AddRowLog(e);
            }

            return ordersList;
        }

        /// <summary>
        /// Показывать подробные сведения о конкретном заказе, включая номенклатуру заказа
        /// </summary>
        public List<InformationOrder> GetFullInformationOrders(int orderID)
        {
            List<InformationOrder> ordersList = new List<InformationOrder>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    SqlCommand command = new SqlCommand(SqlQuery.GetFullInformationOrders, connection);
                    
                    SqlParameter param = new SqlParameter()
                    {
                        ParameterName = "@OrderId",
                        Value = orderID,
                        SqlDbType = SqlDbType.Int
                    };
                    command.Parameters.Add(param);
                    
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            var order = new InformationOrder();
                            order.OrderId = (int)reader["OrderId"];
                            order.ProductId = (int)reader["ProductID"];
                            order.UnitPrice = (decimal)reader["UnitPrice"];
                            order.Quantity = (Int16)reader["Quantity"];
                            order.ProductName = (string)reader["ProductName"];
                            order.SupplierID = (int)reader["SupplierID"];
                            order.CategoryID = (int)reader["CategoryID"];
                            order.QuantityPerUnit = (string)reader["QuantityPerUnit"];
                            order.UnitsInStock = (Int16)reader["UnitsInStock"];
                            order.UnitsOnOrder = (Int16)reader["UnitsOnOrder"];
                            order.ReorderLevel = (Int16)reader["ReorderLevel"];
                            ordersList.Add(order);
                        }
                        
                    }
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                _log.AddRowLog(e);
            }

            return ordersList;
        }

        /// <summary>
        /// Добавить запись в таблицу
        /// </summary>
        public void AddRow(int employeeId, string shipName, string shipCity)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(SqlQuery.AddRow, connection);
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
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                _log.AddRowLog(e);
            }
        }

        /// <summary>
        /// Удалить заказы со статусом "Новый" и "В работе"
        /// </summary>
        public void DeleteOrders()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    SqlCommand command = new SqlCommand(SqlQuery.DeleteOrders, connection);

                    command.ExecuteNonQuery();
                    
                    connection.Close();
                }
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
        public List<CustOrderHist> CustOrderHist(string customerID)
        {
            List<CustOrderHist> custList = new List<CustOrderHist>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    SqlCommand command = new SqlCommand(SqlQuery.CustOrderHist, connection);
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
                            cust.ProductName = (string)reader["ProductName"];
                            cust.Total = (int)reader["Total"];

                            custList.Add(cust);
                        }
                    }         
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                _log.AddRowLog(e);
            }

            return custList;
        }

        /// <summary>
        /// Вызов процедуры "CustOrdersDetail"
        /// </summary>
        /// <param name="orderId">Id заказа</param>
        /// <returns></returns>
        public List<CustOrdersDetail> CustOrdersDetail(int orderId)
        {
            List<CustOrdersDetail> custList = new List<CustOrdersDetail>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    SqlCommand command = new SqlCommand(SqlQuery.CustOrdersDetail, connection);
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
                            cust.ProductName = (string)reader["ProductName"];
                            cust.UnitPrice = (decimal)reader["UnitPrice"];
                            cust.Quantity = (Int16)reader["Quantity"];
                            cust.Discount = (int)reader["Discount"];
                            cust.ExtendedPrice = (decimal)reader["ExtendedPrice"];

                            custList.Add(cust);
                        }
                    }           
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                _log.AddRowLog(e);
            }

            return custList;
        }
    }
}
