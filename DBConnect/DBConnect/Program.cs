using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DBConnect
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Подлючение открыто");
                var log = new ServiceLog(connection);
                var service = new Service(connection, log);
                //service.GetOrders();
                //service.GetFullInformationOrders(10251);
                //service.CreateTable();
                //service.AddRow(5, "Avrora", "Kazan");
                //service.DeleteOrders();
                //service.CustOrderHist("ALFKI");
                service.CustOrdersDetail(10287);
            }
            catch (SqlException e)
            {
                var log = new ServiceLog(connection);
                log.AddRowLog(e);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("Подключение закрыто");
            }
        }
    }
}
