/*1. Написать процедуру, которая возвращает самый крупный заказ для 
каждого из продавцов за определенный год. 
В результатах не может быть несколько заказов одного продавца, должен быть 
только один и самый крупный. 
В результатах запроса должны быть выведены следующие колонки: колонка с 
именем и фамилией продавца (FirstName и LastName – пример: Nancy Davolio), 
номер заказа и его стоимость. 
В запросе надо учитывать Discount при продаже товаров. 
Процедуре передается год, за который надо сделать отчет, и количество 
возвращаемых записей. 
Результаты запроса должны быть упорядочены по убыванию суммы заказа. 
Процедура должна быть реализована 2-мя способами с использованием 
оператора SELECT и с использованием курсора. 
Название процедур соответственно GreatestOrders и GreatestOrdersCur. 
Необходимо продемонстрировать использование этих процедур. 

Также помимо демонстрации вызовов процедур в скрипте Query.sql надо 
написать отдельный ДОПОЛНИТЕЛЬНЫЙ проверочный запрос для тестирования 
правильности работы процедуры GreatestOrders. 
Проверочный запрос должен выводить в удобном для сравнения с результатами 
работы процедур виде для определенного продавца для всех его заказов за 
определенный указанный год в результатах следующие колонки: имя продавца, 
номер заказа, сумму заказа. 
Проверочный запрос не должен повторять запрос, написанный в процедуре, - он 
должен выполнять только то, что описано в требованиях по нему.*/

-- Процедура через SELECT
CREATE OR ALTER PROC GreatestOrders
	@Year INT = 1998
AS
	SELECT 
		CONCAT(e.LastName, ' ', e.FirstName) AS FullName,
		ord.OrderId 						 AS OrderId,
		ord.Price						 	 AS Price
	FROM dbo.Employees e 
	CROSS APPLY (
		SELECT TOP 1
			o.OrderID,
			CONVERT(MONEY,SUM(od.UnitPrice - (od.UnitPrice * (od.Discount*100)/100))) AS Price
		FROM dbo.Orders o 
		JOIN dbo.[Order Details] od 	ON od.OrderID = o.OrderID 
											AND o.EmployeeID = e.EmployeeID 
											AND YEAR(o.OrderDate) = @Year
		GROUP BY o.OrderID, od.UnitPrice 
		ORDER BY od.UnitPrice DESC
	) AS ord
	ORDER BY Price DESC 

-- Проверочный запрос
CREATE OR ALTER FUNCTION GreatestOrdersTest (@EmployeeID INT, @Years INT)
RETURNS @GreatestOrdersTest table
	(
		[Name] NVARCHAR(70) NULL,
		[OrderId] INT NULL,
		[Price] MONEY NULL
	)
AS 
BEGIN 
		INSERT INTO @GreatestOrdersTest
		SELECT
			CONCAT(e.LastName, ' ', e.FirstName) 	AS [Name],
			o.OrderID 								AS [OrderId],
			od.UnitPrice 							AS [Price]
		FROM dbo.Employees e 
		JOIN dbo.Orders o 				ON o.EmployeeID = e.EmployeeID 
		JOIN dbo.[Order Details] od 	ON od.OrderID = o.OrderID 
		WHERE YEAR(o.OrderDate) = @Years AND e.EmployeeID = @EmployeeID
		ORDER BY od.UnitPrice DESC	
		OFFSET 0 ROWS
		
		RETURN;
END

--Процедура через курсор

CREATE OR ALTER PROC GreatestOrdersCur
	@Year INT = 1998
AS
	BEGIN
		DECLARE @FullName NVARCHAR(50)
		DECLARE @OrderId INT
		DECLARE @Price MONEY
		DECLARE @tempPrice MONEY

		CREATE TABLE #greatestOrdersCur
		(FullName NVARCHAR(50),
			OrderId INT,
			Price MONEY)

		DECLARE autocur CURSOR GLOBAL FOR  
			SELECT 
				CONCAT(e.LastName, ' ', e.FirstName)										AS FullName,
				o.OrderID 																	AS OrderId,
				CONVERT(MONEY,SUM(od.UnitPrice - (od.UnitPrice * (od.Discount*100)/100)))	AS UnitPrice
			FROM dbo.Employees e 
			JOIN dbo.Orders o 			ON o.EmployeeID = e.EmployeeID AND YEAR(o.OrderDate) = @Year
			JOIN dbo.[Order Details] od ON od.OrderID = o.OrderID 
			GROUP BY e.LastName, e.FirstName, o.OrderID

		OPEN autocur
		FETCH FROM autocur INTO @FullName, @OrderId, @Price
		WHILE @@FETCH_STATUS = 0   
			BEGIN 
				SET @tempPrice = (SELECT PRICE FROM #greatestOrdersCur WHERE FullName = @FullName)
				IF NOT EXISTS(SELECT * FROM #greatestOrdersCur cur WHERE cur.FullName=@FullName)
					INSERT INTO #greatestOrdersCur VALUES (@FullName, @OrderId, @Price)
				IF @tempPrice < @Price
					UPDATE #greatestOrdersCur SET Price = @Price, OrderId = @OrderId
						FROM #greatestOrdersCur
						WHERE FullName = @FullName 
				FETCH NEXT FROM autocur INTO @FullName, @OrderId, @Price
			END
		CLOSE autocur
		DEALLOCATE autocur
	END

	SELECT *
	FROM #greatestOrdersCur
	
-- Вызов запроса. Первый параметр - EmployeeID(Значения 1-9), второй - Year(1996-1998)
SELECT *
FROM dbo.GreatestOrdersTest(2, 1998)

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

CREATE OR ALTER PROC ShippedOrdersDiff
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
	
-- ИЛИ 

IF NOT EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'[dbo].[ShippedOrdersDiff]') AND TYPE IN (N'P', N'PC'))
EXEC('CREATE PROCEDURE [dbo].[ShippedOrdersDiff] AS')
GO
	ALTER PROCEDURE [dbo].[ShippedOrdersDiff]
	@SpecifiedDelay INT = 35
AS
BEGIN
	SELECT
		o.OrderID 									AS OrderID,
		o.OrderDate 								AS OrderDate,
		o.ShippedDate 								AS ShippedDate,
		SUM(DAY(o.ShippedDate) - DAY(o.OrderDate))	AS ShippedDelay,
		@SpecifiedDelay								AS SpecifiedDelay
	FROM dbo.Orders o
	WHERE DAY(o.ShippedDate) - DAY(o.OrderDate) > 0 AND DAY(o.ShippedDate) - DAY(o.OrderDate) <= @SpecifiedDelay
	GROUP BY o.OrderID, o.OrderDate, o.ShippedDate 
END
	
-- EXECUTE ShippedOrdersDiff 

/*3. Написать функцию, которая определяет, есть ли у продавца подчиненные. 
Возвращает тип данных BIT. В качестве входного параметра функции используется 
EmployeeID. Название функции IsBoss. 
Продемонстрировать использование функции для всех продавцов из таблицы 
Employees.*/

CREATE OR ALTER FUNCTION IsBoss (@EmployeeID INT)
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

CREATE OR ALTER VIEW ViewTest AS
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

/*5. Создать таблицу dbo.OrdersHistory, которая будет хранить историю 
изменений по таблице dbo.Orders. 
Необходимо подумать какие бы поля могла бы содержать данная таблица. 
Обосновать свой выбор. Почему именно такой набор полей должен быть в 
таблице dbo.OrdersHistory? 
Затем для таблицы dbo.Orders необходимо создать триггер, который при любом 
изменении данных в таблице dbo.Orders будет записывать значения в новую 
таблицу dbo.OrdersHistory. 
Написать проверочный запрос, который будет изменять/удалять данные из 
таблицы dbo.Order*/

-- Создание таблицы
CREATE TABLE dbo.OrdersHistory
(
	Id INT IDENTITY (1,1),
	OrderId INT, -- Новый OrderId
	OrderIdOld INT, -- Старый OrderId
	CustomerId NCHAR(5), -- Новый CustomerId
	CustomerIdOld NCHAR(5), -- Старый CustomerId
	EmployeeId INT, -- Новый EmployeeId
	EmployeeIdOld INT, -- Старый EmployeeId
	OrderDate DATETIME, -- Новый OrderDate
	OrderDateOld DATETIME, -- Старый OrderDate
	Freight MONEY, -- Новый Freight
	FreightOld MONEY, -- Старый Freight
	ShipName NVARCHAR(40), -- Новый ShipName
	ShipNameOld NVARCHAR(40), -- Старый ShipName
	ShipAddress NVARCHAR(60), -- Новый ShipAddress
	ShipAddressOld NVARCHAR(60) -- Старый ShipAddress
)

--Триггер на обновление
CREATE TRIGGER Orders_UPDATE
ON dbo.Orders AFTER UPDATE
AS
	BEGIN
		DECLARE @OrderId INT = (SELECT OrderId FROM Deleted)
		DECLARE @OrderIdOld INT = (SELECT OrderId FROM Inserted)
		DECLARE @CustomerId NCHAR(5) = (SELECT CustomerId FROM Deleted)
		DECLARE @CustomerIdOld NCHAR(5) = (SELECT CustomerId FROM Inserted)
		DECLARE @EmployeeId INT = (SELECT EmployeeId FROM Deleted)
		DECLARE @EmployeeIdOld INT = (SELECT EmployeeId FROM Inserted)
		DECLARE @OrderDate DATETIME = (SELECT OrderDate FROM Deleted)
		DECLARE @OrderDateOld DATETIME = (SELECT OrderDate FROM Inserted)
		DECLARE @Freight MONEY = (SELECT Freight FROM Deleted)
		DECLARE @FreightOld MONEY = (SELECT Freight FROM Inserted)
		DECLARE @ShipName NVARCHAR(50) = (SELECT ShipName FROM Deleted)
		DECLARE @ShipNameOld NVARCHAR(50) = (SELECT ShipName FROM Inserted)
		DECLARE @ShipAddress NVARCHAR(60) = (SELECT ShipAddress FROM Deleted)
		DECLARE @ShipAddressOld NVARCHAR(60) = (SELECT ShipAddress FROM Inserted)

		INSERT INTO dbo.OrdersHistory 
		VALUES (@OrderId, @OrderIdOld,
				@CustomerId, @CustomerIdOld,
				@EmployeeId, @EmployeeIdOld,
				@OrderDate, @OrderDateOld,
				@Freight, @FreightOld,
				@ShipName, @ShipNameOld,
				@ShipAddress, @ShipAddressOld,
				GETDATE(), 'Update')
	END
	
-- Триггер на удаление
CREATE TRIGGER Orders_DELETE
ON dbo.Orders AFTER DELETE
AS
	BEGIN
		DECLARE @OrderIdOld INT = (SELECT OrderId FROM Deleted)
		DECLARE @CustomerIdOld NCHAR(5) = (SELECT CustomerId FROM Deleted)
		DECLARE @EmployeeIdOld INT = (SELECT EmployeeId FROM Deleted)
		DECLARE @OrderDateOld DATETIME = (SELECT OrderDate FROM Deleted)
		DECLARE @FreightOld MONEY = (SELECT Freight FROM Deleted)
		DECLARE @ShipNameOld NVARCHAR(50) = (SELECT ShipName FROM Deleted)
		DECLARE @ShipAddressOld NVARCHAR(60) = (SELECT ShipAddress FROM Deleted)

		INSERT INTO dbo.OrdersHistory 
		VALUES (NULL, @OrderIdOld,
				NULL, @CustomerIdOld,
				NULL, @EmployeeIdOld,
				NULL, @OrderDateOld,
				NULL, @FreightOld,
				NULL, @ShipNameOld,
				NULL, @ShipAddressOld,
				GETDATE(), 'Delete')
	END

--Триггер на добавление
CREATE TRIGGER Orders_INSERT
ON dbo.Orders AFTER INSERT
AS
	BEGIN
		DECLARE @OrderId INT = (SELECT OrderId FROM Inserted)
		DECLARE @CustomerId NCHAR(5) = (SELECT CustomerId FROM Inserted)
		DECLARE @EmployeeId INT = (SELECT EmployeeId FROM Inserted)
		DECLARE @OrderDate DATETIME = (SELECT OrderDate FROM Inserted)
		DECLARE @Freight MONEY = (SELECT Freight FROM Inserted)
		DECLARE @ShipName NVARCHAR(50) = (SELECT ShipName FROM Inserted)
		DECLARE @ShipAddress NVARCHAR(60) = (SELECT ShipAddress FROM Inserted)

		INSERT INTO dbo.OrdersHistory 
		VALUES (@OrderId, NULL,
				@CustomerId, NULL,
				@EmployeeId, NULL,
				@OrderDate, NULL,
				@Freight, NULL,
				@ShipName, NULL,
				@ShipAddress, NULL,
				GETDATE(), 'Insert')
	END
	
	