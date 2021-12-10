/*4.1.1.Написать запрос, который добавляет новый заказ в таблицу dbo.Orders
Необходимо написать два запроса
• первый с использованием Values;
• второй с использованием Select*/
INSERT INTO dbo.Orders 
VALUES (N'RATTC',1,'8/2/2021','8/12/2021','8/8/2021',3,50.00,
	N'Avrora',N'5ª St. Petersburg',N'Petersburg',
	N'RU',N'1081',N'Russia')
	
INSERT INTO dbo.Orders DEFAULT VALUES
SELECT *
FROM dbo.Orders
	
/*4.1.2.Написать запрос, который добавляет 5 новых заказов в таблицу dbo.Orders
Необходимо написать два запроса
• первый с использованием Values;
• второй с использованием Select.*/
DECLARE @rows int, @cntr int 
SET @rows = 5
SET @cntr = 0

WHILE @cntr < @rows
BEGIN
	INSERT INTO dbo.Orders 
	VALUES (N'RATTC',1,'8/2/2021','8/12/2021','8/8/2021',3,50.00,
		N'Avrora',N'5ª St. Petersburg',N'Petersburg',
		N'RU',N'1081',N'Russia')
	SET @cntr = @cntr + 1
END

INSERT INTO dbo.Orders
SELECT TOP (5) 'TOMSP',
EmployeeID, OrderDate, RequiredDate,
ShippedDate, ShipVia, Freight,
ShipName, ShipAddress, ShipCity,
ShipRegion, ShipPostalCode, ShipCountry
FROM dbo.Orders

/*4.1.3.Написать запрос, который добавляет в таблицу dbo.Orders дублирующие 
заказы по CustomerID = ‘WARTH’ и продавцу EmployeeID = 5 (заменить 
CustomerID на ‘TOMSP’).*/ 

INSERT INTO dbo.Orders
SELECT 'TOMSP',
EmployeeID, OrderDate, RequiredDate,
ShippedDate, ShipVia, Freight,
ShipName, ShipAddress, ShipCity,
ShipRegion, ShipPostalCode, ShipCountry
FROM dbo.Orders
WHERE CustomerID = N'WARTH' AND EmployeeID = 5
	
/*4.1.4.Написать запрос, который обновит по всем заказам дату ShippedDate
(которые еще не доставлены) на текущую дату.*/
UPDATE dbo.Orders SET ShippedDate = GETDATE()
FROM dbo.Orders o
WHERE o.ShippedDate IS NULL 

/*4.1.5.Написать запрос, который обновит скидку на произвольное значение (поле 
Discount таблицы dbo.[Order Details]) по заказам, где CustomerID = ‘GODOS’ 
по продукту ‘Tarte au sucre’.*/
UPDATE dbo.[Order Details] SET Discount = 0.15
FROM dbo.Products p 
LEFT JOIN dbo.[Order Details] od 		ON od.ProductID = p.ProductID 
LEFT JOIN dbo.Orders o 					ON o.OrderID = od.OrderID
WHERE p.ProductName = 'Tarte au sucre' AND o.CustomerID = 'GODOS'

-- 4.1.6.Написать запрос, который удалит заказы, по которым сумма заказа меньше 20
DELETE FROM dbo.[Order Details] 
WHERE Quantity < 20

/*4.2.1. Необходимо создать и заполнить две временные таблицы:
#tblBookInLibrary – содержит информацию о дате регистрации по 
некоторым книгам в библиотеке
#tblBook – содержит информацию о книгах, которые есть в библиотеке
Используя данные из этих таблиц необходимо написать запросы:
Запрос 1. Выбрать все книги, а поле дата должно быть заполнено только у 
тех книг, 
у которых дата регистрации больше 01.02.2005
Запрос 2. Выбрать все книги у которых дата регистрации в библиотеке 
больше 01.02.2005, либо не задана вообще*/

-- Создание временной таблицы tblBook
CREATE TABLE #tblBook
(BookId INT,
Name NVARCHAR(40))

-- Создание временной таблицы tblBookInLibrary
CREATE TABLE #tblBookInLibrary
(BookId INT,
Date DATETIME)
 
-- Добавление данных во временную таблицу tblBook
INSERT INTO #tblBook
VALUES  (1, N'Война и мир'),
        (2, N'Преступление и наказание'),
        (3, N'Мастер и Маргарита'),
        (4, N'Тихий дон')
 
-- Добавление данных во временную таблицу tblBookInLibrary       
INSERT INTO #tblBookInLibrary
VALUES  (1,CAST('2006-05-01' as datetime)),
        (3,CAST('2004-07-05' as datetime))
        
-- Первый запрос
SELECT
	tb.Name 	AS Name,
	tbil.Date 	AS Date
FROM #tblBook tb
LEFT JOIN #tblBookInLibrary tbil 	ON tbil.BookId = tb.BookId AND tbil.Date > CAST('2005-01-02' as datetime)

-- Второй запрос 
SELECT
	tb.Name 	AS Name,
	tbil.Date 	AS Date
FROM #tblBook tb
LEFT JOIN #tblBookInLibrary tbil 	ON tbil.BookId = tb.BookId
WHERE tbil.Date > CAST('2005-01-02' as datetime) OR tbil.Date IS NULL