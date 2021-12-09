/*2. Написать процедуру, которая возвращает заказы в таблице Orders, 
согласно указанному сроку доставки в днях (разница между OrderDate и 
ShippedDate). 
В результатах должны быть возвращены заказы, срок которых превышает 
переданное значение или еще недоставленные заказы. 
Значению по умолчанию для передаваемого срока 35 дней. 
Название процедуры ShippedOrdersDiff. 
Процедура должна высвечивать следующие колонки: OrderID, OrderDate,
ShippedDate, ShippedDelay (разность в днях между ShippedDate и OrderDate), 
SpecifiedDelay (переданное в процедуру значение). 
Необходимо продемонстрировать использование этой процедуры*/

CREATE PROC ShippedOrdersDiff
	@SpecifiedDelay INT = 35
AS
	SELECT
		o.OrderID 									AS OrderID,
		o.OrderDate 								AS OrderDate,
		o.ShippedDate 								AS ShippedDate,
		SUM(DAY(o.ShippedDate) - DAY(o.OrderDate))	AS ShippedDelay,
		@SpecifiedDelay								AS SpecifiedDelay
	FROM dbo.Orders o
	WHERE DAY(o.ShippedDate) - DAY(o.OrderDate) > 0 AND DAY(o.ShippedDate) - DAY(o.OrderDate) <= @SpecifiedDelay
	GROUP BY o.OrderID, o.OrderDate, o.ShippedDate 
	
-- EXECUTE ShippedOrdersDiff 

/*3. Написать функцию, которая определяет, есть ли у продавца подчиненные. 
Возвращает тип данных BIT. В качестве входного параметра функции используется 
EmployeeID. Название функции IsBoss. 
Продемонстрировать использование функции для всех продавцов из таблицы 
Employees.*/

CREATE FUNCTION IsBoss (@EmployeeID INT)
RETURNS BIT 
AS 
BEGIN 
	DECLARE @Total INT
	SELECT @Total = COUNT(*)
	FROM Northwind.dbo.Employees emp
	WHERE emp.ReportsTo = @EmployeeID 
	RETURN @Total
END

/*4. Написать запрос, который должен вывести следующие поля: 
• OrderID (dbo.Orders), 
• CustomerID (dbo.Orders), 
• LastName + FirstName (dbo.Employees), 
• OrderDate (dbo.Orders), 
• RequiredDate (dbo.Orders), 
• ProductName (dbo.Products), 
• цену товара с учетом скидки. 
Результат запроса необходимо представить в виде представления. 
Отсортировать по цене товара.*/

CREATE VIEW ViewTest AS
SELECT 
	o.OrderID 														AS OrderId,
	o.CustomerID													AS CustomerID,
	CONCAT(e.LastName, ' ',e.FirstName)								AS [LastName + FirsName],
	o.OrderDate 													AS OrderDate,
	o.RequiredDate 													AS RequiredDate,
	p.ProductName 													AS ProductName,
	SUM(od.UnitPrice - (od.UnitPrice * (od.Discount*100)/100))		AS Product
FROM dbo.[Order Details] od 
LEFT JOIN dbo.Orders o 			ON o.OrderID = od.OrderID 
LEFT JOIN dbo.Employees e 		ON e.EmployeeID = o.EmployeeID 
LEFT JOIN dbo.Products p 		ON p.ProductID = od.ProductID
GROUP BY o.OrderID, o.CustomerID, e.LastName, e.FirstName, o.OrderDate, o.RequiredDate, p.ProductName 
ORDER BY Product
OFFSET 0 ROWS