using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Ado.Net.Logger.Interfaces;

namespace Ado.Net.Logger
{
    /// <summary>
    /// Сервис логирования
    /// </summary>
    public class ServiceLog : IServiceLog
    {
        private readonly string _connectionString;

        public ServiceLog()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            CreateLogTable();
        }

        /// <summary>
        /// Создание таблицы для логирования
        /// </summary>
        public void CreateLogTable()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    SqlCommand command = new SqlCommand(CreateTable, connection);
                    command.ExecuteNonQuery();
                    
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Метод логирования
        /// </summary>
        /// <param name="e">Исключительная ситуация</param>
        public void AddRowLog(Exception e)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(AddLog, connection);
                    SqlParameter param = new SqlParameter()
                    {
                        ParameterName = "@Message",
                        Value = e.Message,
                        SqlDbType = SqlDbType.NVarChar
                    };
                    command.Parameters.Add(param);

                    param = new SqlParameter()
                    {
                        ParameterName = "@StackTrace",
                        Value = e.StackTrace,
                        SqlDbType = SqlDbType.NVarChar
                    };
                    command.Parameters.Add(param);

                    command.ExecuteNonQuery();
                    
                    connection.Close();
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                throw;
            }
        }

        private string AddLog = $@"INSERT 
				INTO dbo.Log (Message, StackTrace, Date)
				VALUES(@Message, @StackTrace, GETDATE())";

        private string CreateTable = $@"
		IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Log')
		BEGIN
				CREATE TABLE Log
				(Id INT IDENTITY(1,1) PRIMARY KEY,
				 Message VARCHAR(5000),
				 StackTrace VARCHAR(5000),
				 Date DATETIME)
		END";
    }
}