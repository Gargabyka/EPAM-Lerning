using Ado.Net.Contracts.Enums;

namespace Ado.Net.SqlHelper
{
    public static class SqlQuery
    {
        /// <summary>
        /// Получить информацию о заказах
        /// </summary>
        public static string GetOrders = $@"SELECT 
				o.OrderID,o.Freight,o.ShipName, o.ShipName,
				o.ShipAddress, o.ShipCity, o.ShipCountry, o.OrderDate, o.ShippedDate,
				CASE
					WHEN o.OrderDate IS NULL THEN {(int)StateOrder.New}
					WHEN o.ShippedDate IS NULL THEN {(int)StateOrder.AtWork} 
					WHEN o.ShippedDate  IS NOT NULL THEN {(int)StateOrder.Completed}
				END AS State
			   FROM dbo.Orders o";

        /// <summary>
        /// Получить полную информацию о заказах
        /// </summary>
        public static string GetFullInformationOrders = $@"SELECT
				    od.OrderID, od.ProductID, od.UnitPrice, od.Quantity, 
				    p.ProductName, p.SupplierID, p.CategoryID, p.QuantityPerUnit, 
				    p.UnitsInStock, p.UnitsOnOrder, p.ReorderLevel 
				FROM [Order Details] od 
				JOIN Products p ON od.ProductID = p.ProductID
				WHERE od.OrderId = @OrderId";

        /// <summary>
        /// Создать таблицу
        /// </summary>
        public static string CreateTable = $@"CREATE TABLE Log
							(Id INT IDENTITY(1,1) PRIMARY KEY,
							 Message VARCHAR(500),
							 StackTrace VARCHAR(500),
							 Date DATETIME)";

        /// <summary>
        /// Добавить строку в таблицу Orders
        /// </summary>
        public static string AddRow = $@"INSERT 
							INTO dbo.Orders (EmployeeID, ShipName, ShipCity)
							VALUES(@EmployeeID, @ShipName, @ShipCity)";

        /// <summary>
        /// Удалить заказы со статусом "Новый" и "В работе"
        /// </summary>
        public static string DeleteOrders = $@"DELETE FROM dbo.Orders 
						WHERE ShippedDate IS NULL AND OrderDate IS NULL";

        /// <summary>
        /// Вызов процедуры CustOrderHist
        /// </summary>
        public static string CustOrderHist = $@"EXECUTE CustOrderHist @CustomerID";

        /// <summary>
        /// Вызов процедуры CustOrderHist
        /// </summary>
        public static string CustOrdersDetail = $@"EXECUTE CustOrdersDetail @OrderID";
    }
}
