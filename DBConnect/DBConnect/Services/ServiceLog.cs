using System;
using System.Data;
using System.Data.SqlClient;


namespace DBConnect
{
	/// <summary>
	/// Сервис логирования
	/// </summary>
    public class ServiceLog
    {
	    private readonly SqlConnection _connection;
        
        public ServiceLog(SqlConnection connection)
        {
            _connection = connection;
            CreateLogTable();
        }

        /// <summary>
        /// Создание таблицы для логирования
        /// </summary>
        public void CreateLogTable()
        {
	        SqlCommand command = new SqlCommand(CreateTable, _connection);
	        try
	        {
		        command.ExecuteNonQuery();
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
	        SqlCommand command = new SqlCommand(AddLog, _connection);
	        try
	        {
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