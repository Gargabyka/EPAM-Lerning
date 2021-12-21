using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Ado.Net
{
    public class DBConnect
    {
        public DBConnect()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Подлючение открыто");
                DataTable tblOrders;
                string query = "SELECT * FROM dbo.Orders o";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Не удалось подключиться");
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("Подключение закрыто");
            }
        }
    }
}