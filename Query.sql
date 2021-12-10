-- 2.1.1. Выбрать в таблице Orders заказы, которые одновременно удовлетворяют 
-- условиям:
-- • были доставлены после 6 мая 1998 года (колонка ShippedDate) включительно;
-- • доставлены с ShipVia >= 2. 
-- Формат указания даты должен быть верным при любых региональных настройках, 
-- согласно требованиям статьи “Writing International Transact-SQL Statements” в Books
-- Online раздел “Accessing and Changing Relational Data Overview”. 
-- Этот метод использовать далее для всех заданий. 
-- Запрос должен высвечивать только колонки:
-- • OrderID;
-- • ShippedDate;
-- • ShipVia. 
-- Пояснить почему сюда не попали заказы с NULL-ом в колонке ShippedDate

	SELECT ord.OrderID, ord.ShippedDate, ord.ShipVia
	FROM dbo.Orders ord
	WHERE ord.ShipVia >= 2 and ord.ShippedDate >= N'19980506'

-- Не попали значения NULL, т.к. нет условий подходящих для вывода данного значения 

-- 2.1.2  Написать запрос, который выводит только недоставленные заказы из таблицы 
/*Orders (т.е. в колонке ShippedDate нет значения даты доставки). 
Запрос должен высвечивать только колонки:
• OrderID;
• ShippedDate.
Для колонки ShippedDate вместо значений NULL выводить строку ‘Not Shipped’ – для 
этого использовать системную функцию CASЕ.*/

		SELECT 
		ord.OrderID, 
			CASE
				WHEN ord.ShippedDate IS NULL THEN 'Not Shipped'   
			END 
		AS ShippedDate
	FROM dbo.Orders ord
	WHERE ShippedDate IS NULL;
	
-- 2.1.3 Выбрать в таблице Orders заказы, которые удовлетворяют хотя бы одному из 
/*условий: 
• были доставлены после 6 мая 1998 года (колонка ShippedDate) не включая эту 
дату;
• еще не доставлены. 
Запрос должен высвечивать только колонки:
• OrderID (переименовать в Order Number);
• ShippedDate (переименовать в Shipped Date).
Для колонки ShippedDate вместо значений NULL выводить строку ‘Not Shipped’, для 
остальных значений высвечивать дату в формате по умолчанию.
2.2. Использование операторов IN, DISTINCT, ORDER BY, NOT*/

	SELECT 
		ord.OrderID as [Order Number],
			CASE
					WHEN ord.ShippedDate IS NULL THEN 'Not Shipped'   
					ELSE CONVERT(char, ord.ShippedDate, 102)
			END 
		as [Shippend Date]
	FROM dbo.Orders ord
	WHERE ord.ShippedDate > N'19980506'  OR ord.ShippedDate IS NULL;
	
/*2.2.1  Выбрать из таблицы Customers всех заказчиков, проживающих в USA и Canada. 
Запрос сделать с только помощью оператора IN. 
Высвечивать колонки с именем пользователя и названием страны в результатах запроса. 
Упорядочить результаты запроса по имени заказчиков и по месту проживания*/

	SELECT 
		cus.ContactName AS [User Name],
		cus.Country 	AS [User Country]
	FROM dbo.Customers cus 
	WHERE cus.Country in ('USA', 'Canada')
	ORDER BY cus.Country, cus.ContactName

/* 2.2.2  Выбрать из таблицы Customers всех заказчиков, не проживающих в USA и Canada. 
Запрос сделать с помощью оператора IN. 
Высвечивать колонки с именем пользователя и названием страны в результатах запроса. 
Упорядочить результаты запроса по имени заказчиков*/
	
	SELECT 
		cus.ContactName AS [User Name],
		cus.Country AS [User Country]
	FROM dbo.Customers cus 
	WHERE cus.Country not in ('USA', 'Canada')
	ORDER BY cus.ContactName
	
-- 2.2.3 Выбрать из таблицы Customers все страны, в которых проживают заказчики. 
/*Страна должна быть упомянута только один раз. 
Cписок стран должен быть отсортирован по убыванию. 
Не использовать предложение GROUP BY. 
Высвечивать только одну колонку в результатах запроса.*/
	
	SELECT DISTINCT
		cus.Country AS [User Country] 
	FROM dbo.Customers cus 
	ORDER BY cus.Country DESC
	
-- 2.3.1 Выбрать все заказы (OrderID) из таблицы Order Details (заказы не должны 
/*повторяться), где встречаются продукты с количеством от 3 до 10 включительно – это 
колонка Quantity в таблице Order Details. 
Использовать оператор BETWEEN. 
Запрос должен высвечивать только колонку OrderID.*/

	SELECT DISTINCT
		ordet.OrderID
	FROM dbo.[Order Details] ordet
	WHERE ordet.Quantity BETWEEN 3 AND 10
	
-- 2.3.2  Выбрать всех заказчиков из таблицы Customers, у которых название страны 
/*начинается на буквы из диапазона b и g. 
Использовать оператор BETWEEN. 
Проверить, что в результаты запроса попадает Germany. 
Запрос должен высвечивать только колонки
• CustomerID;
• Country.
Результат запроса должен быть отсортирован по Country.*/
	
	SELECT 
		cust.CustomerID AS [UserId],
		cust.Country	AS [User Country]
	FROM dbo.Customers cust
	WHERE cust.Country BETWEEN 'b' AND 'h'
	ORDER BY cust.Country
	-- План запроса
	-- SELECT [cust].[CustomerID] [UserId],[cust].[Country] [User's Country] FROM [dbo].[Customers] [cust] 
	-- WHERE [cust].[Country]>=@1 AND [cust].[Country]<=@2 ORDER BY [cust].[Country] ASC

	
-- 2.3.3 Выбрать всех заказчиков из таблицы Customers, у которых название страны 
/*начинается на буквы из диапазона b и g. 
Не используя оператор BETWEEN. 
Запрос должен высвечивать только колонки
• CustomerID;
• Country.
Результат запроса должен быть отсортирован по Country.
С помощью опции “Execution Plan” определить какой запрос предпочтительнее 2.3.2 или 
2.3.3. Для этого надо ввести в скрипт выполнение текстового Execution Plan-a для двух 
этих запросов. Результаты выполнения Execution Plan надо ввести в скрипт в виде 
комментария и по их результатам дать ответ на вопрос – по какому параметру было 
проведено сравнение. 
Запрос должен высвечивать только колонки CustomerID и Country и отсортирован по 
Country.*/

	SELECT 
		cust.CustomerID AS [UserId],
		cust.Country	AS [User Country]
	FROM dbo.Customers cust
	WHERE cust.Country >= 'b' AND cust.Country <= 'h'
	ORDER BY cust.Country
	-- План запроса
	-- SELECT [cust].[CustomerID] [UserId],[cust].[Country] [User's Country] FROM [dbo].[Customers] [cust] 
	-- WHERE [cust].[Country]>=@1 AND [cust].[Country]<=@2 ORDER BY [cust].[Country] ASC
	
-- 2.4.1 В таблице Products найти все продукты (колонка ProductName), где встречается 
/*подстрока 'chocolade'. Известно, что в подстроке 'chocolade' может быть изменена одна 
буква 'c' в середине - найти все продукты, которые удовлетворяют этому условию. 
Подсказка: результаты запроса должны высвечивать 2 строки.*/

	SELECT 
		pro.ProductName AS [Product Name]
	FROM dbo.Products pro
	WHERE pro.ProductName LIKE '%cho_olade%'
	
-- 2.4.2  Для формирования алфавитного указателя Employees высветить из таблицы 
/*Employees список только тех букв алфавита, с которых начинаются фамилии Employees 
(колонка LastName ) из этой таблицы. 
Алфавитный список должен быть отсортирован по возрастанию.*/
	
	SELECT DISTINCT 
		SUBSTRING(emp.LastName, 1,1) AS [Last Name]
	FROM dbo.Employees emp
	ORDER BY [Last Name]
	
-- 2.5.1 Определить продавцов, которые обслуживают регион 'Western' (таблица Region). 
/*Результаты запроса должны высвечивать поля: 
• 'LastName' продавца;
• название обслуживаемой территории ('TerritoryDescription' из таблицы 
Territories).
Запрос должен использовать JOIN в предложении FROM. 
Для определения связей между таблицами Employees и Territories надо использовать 
графические диаграммы для базы Northwind*/
	
	SELECT 
		e.LastName 				AS [Last Name],
		t.TerritoryDescription 	AS [Territory Description]
	FROM dbo.Region r 
	LEFT JOIN dbo.Territories t 			ON t.RegionID = r.RegionID 
	LEFT JOIN dbo.EmployeeTerritories et 	ON et.TerritoryID = t.TerritoryID 
	LEFT JOIN dbo.Employees e 				ON e.EmployeeID = et.EmployeeID 
	WHERE r.RegionDescription IN ('Western')
	ORDER BY t.TerritoryDescription 
	
-- 2.5.2  Высветить в результатах запроса имена всех заказчиков из таблицы Customers и 
/*суммарное количество их заказов из таблицы Orders. 
Принять во внимание, что у некоторых заказчиков нет заказов, но они также должны 
быть выведены в результатах запроса. 
Упорядочить результаты запроса по возрастанию количества заказов*/
	
	SELECT  
		c.CompanyName 		AS [Company Name],
		COUNT(o.CustomerID) AS Count
	FROM dbo.Customers c 
	LEFT JOIN dbo.Orders o 		ON o.CustomerID = c.CustomerID
	GROUP BY o.CustomerID, c.CompanyName 
	ORDER BY Count
	
-- 2.5.3 Высветить всех поставщиков колонка CompanyName в таблице Suppliers, у 
-- которых нет хотя бы одного продукта на складе (UnitsInStock в таблице Products равно 0).
	SELECT 
		s.CompanyName AS [Company Name]
	FROM dbo.Products p 
	LEFT JOIN dbo.Suppliers s 	ON s.SupplierID = p.SupplierID 
	WHERE p.UnitsInStock = 0

