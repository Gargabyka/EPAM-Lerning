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
	WHERE ord.ShipVia >= 2 and ord.ShippedDate >= CONVERT(DATETIME, '19980506');

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
		AS ord.ShippedDate
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
	WHERE ord.ShippedDate >= CONVERT(DATETIME, '19980506')  OR ord.ShippedDate IS NULL;
	
-- 2.2.1  Выбрать из таблицы Customers всех заказчиков, проживающих в USA и Canada. 
/*Запрос сделать с только помощью оператора IN. 
Высвечивать колонки с именем пользователя и названием страны в результатах запроса. 
Упорядочить результаты запроса по имени заказчиков и по месту проживания*/

	SELECT 
		cus.ContactName AS [User Name],
		cus.Country 	AS [User's Country]
	FROM dbo.Customers cus 
	WHERE cus.Country in ('USA', 'Canada')
	ORDER BY cus.Country, cus.ContactName

-- 2.2.2  Выбрать из таблицы Customers всех заказчиков, не проживающих в USA и Canada. 
/*Запрос сделать с помощью оператора IN. 
Высвечивать колонки с именем пользователя и названием страны в результатах запроса. 
Упорядочить результаты запроса по имени заказчиков*/
	
	SELECT 
		cus.ContactName AS [User Name],
		cus.Country AS [User's Country]
	FROM dbo.Customers cus 
	WHERE cus.Country not in ('USA', 'Canada')
	ORDER BY cus.ContactName
	
-- 2.2.3 Выбрать из таблицы Customers все страны, в которых проживают заказчики. 
/*Страна должна быть упомянута только один раз. 
Cписок стран должен быть отсортирован по убыванию. 
Не использовать предложение GROUP BY. 
Высвечивать только одну колонку в результатах запроса.*/
	
	SELECT DISTINCT
		cus.Country AS [User's Country] 
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
		cust.Country	AS [User's Country]
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
		cust.Country	AS [User's Country]
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
	
	SELECT 
		SUBSTRING(emp.LastName, 1,1) AS [Last Name]
	FROM dbo.Employees emp
	ORDER BY emp.LastName
	
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
	
-- 3.1.1  Найти общую сумму всех заказов из таблицы Order Details с учетом 
/*количества закупленных товаров и скидок по ним. 
Результат округлить до сотых и высветить в стиле 1 для типа данных money. 
Скидка (колонка Discount) составляет процент из стоимости для данного товара. 
Для определения действительной цены на проданный продукт надо вычесть 
скидку из указанной в колонке UnitPrice цены. 
Результатом запроса должна быть одна запись с одной колонкой с названием 
колонки 'Totals'.*/
	
	SELECT 
		SUM(CAST(ROUND(((od.UnitPrice - (od.UnitPrice * (od.Discount*100)/100)) * od.Quantity), 1) AS MONEY)) AS Total
	FROM dbo.[Order Details] od 

-- 3.1.2  По таблице Orders найти количество заказов, которые еще не были 
/*доставлены (т.е. в колонке ShippedDate нет значения даты доставки). 
Использовать при этом запросе только оператор COUNT. 
Не использовать предложения WHERE и GROUP.*/
	
	SELECT 
		COUNT(*)-COUNT(o.ShippedDate) AS Count
	FROM dbo.Orders o 
	
-- 3.1.3 По таблице Orders найти количество различных покупателей (CustomerID), 
/*сделавших заказы. 
Использовать функцию COUNT и не использовать предложения WHERE и GROUP.*/
	
	SELECT
		COUNT(DISTINCT o.CustomerID) AS Count
	FROM dbo.Orders o 
	
-- 3.2.1 По таблице Orders найти количество заказов с группировкой по годам. 
/*В результатах запроса надо высвечивать две колонки c названиями Year и Total. 
Написать проверочный запрос, который вычисляет количество всех заказов.*/
	
	SELECT 
		DATEPART(YEAR, o.OrderDate) 			AS Year,
		COUNT(o.OrderID) 						AS Total
	FROM dbo.Orders o 
	GROUP BY DATEPART(YEAR, o.OrderDate)
	
-- 3.2.2 По таблице Orders найти количество заказов, оформленных каждым 
/*продавцом. 
Заказ для указанного продавца – это любая запись в таблице Orders, где в колонке 
EmployeeID задано значение для данного продавца.
В результатах запроса надо высвечивать колонку с именем продавца (Должно 
высвечиваться имя полученное конкатенацией LastName & FirstName. Эта строка 
LastName & FirstName должна быть получена отдельным запросом в колонке 
основного запроса. Также основной запрос должен использовать группировку 
по EmployeeID.) с названием колонки ‘Seller’ и колонку c количеством заказов 
высвечивать с названием 'Amount'. 
Результаты запроса должны быть упорядочены по убыванию количества заказов.*/
	
	SELECT 
		(SELECT CONCAT(e.LastName, ' ', e.FirstName) 
			FROM dbo.Employees e
			WHERE e.EmployeeID = o.EmployeeID) AS Seller,
		COUNT(o.EmployeeID)					   AS Amount 
	FROM dbo.Orders o 
	GROUP BY o.EmployeeID 
	
/*3.2.3 По таблице Orders найти количество заказов
Условия:
• Заказы сделаны каждым продавцом и для каждого покупателя;
• Заказы сделаны в 1998 году.
В результатах запроса надо высвечивать:
• Колонку с именем продавца (название колонки ‘Seller’);
• Колонку с именем покупателя (название колонки ‘Customer’);
• Колонку c количеством заказов высвечивать с названием 'Amount'.
В запросе необходимо использовать специальный оператор языка T-SQL для 
работы с выражением GROUP (Этот же оператор поможет выводить строку “ALL” 
в результатах запроса). 
Группировки должны быть сделаны по ID продавца и покупателя. 
Результаты запроса должны быть упорядочены по: 
• Продавцу;
• Покупателю;
• убыванию количества продаж. 
В результатах должна быть сводная информация по продажам.:
Seller Customer Amount
ALL ALL <общее число продаж>
<имя> ALL <число продаж для данного продавца>
ALL <имя> <число продаж для данного покупателя>
<имя> <имя> <число продаж данного продавца для даннного покупателя>*/
	
	SELECT 
		(SELECT CONCAT(e.LastName, ' ', e.FirstName) 
				FROM dbo.Employees e
				WHERE e.EmployeeID = o.EmployeeID) AS Seller,
		(SELECT c.CompanyName 
				FROM dbo.Customers c
				WHERE c.CustomerID = o.CustomerID) AS Customer,
		COUNT(*)								   AS Amount
	FROM dbo.Orders o 
	WHERE DATEPART(YEAR, o.OrderDate) = 1998
	GROUP BY CUBE(o.EmployeeID, o.CustomerID)
	ORDER BY Seller ASC, Customer ASC, Amount DESC

/*3.2.4. Найти покупателей и продавцов, которые живут в одном городе. 
Если в городе живут только продавцы или только покупатели, то информация о 
таких покупателя и продавцах не должна попадать в результирующий набор. 
В результатах запроса необходимо вывести следующие заголовки для результатов 
запроса: 
• ‘Person’;
• ‘Type’ (здесь надо выводить строку ‘Customer’ или ‘Seller’ в завимости от 
типа записи);
• ‘City’. 
Отсортировать результаты запроса по колонке ‘City’ и по ‘Person’*/
	
CONCAT(e.LastName, ' ', e.FirstName) AS Person,
	'Seller'							 AS Type,
	e.City 								 AS City 
	FROM dbo.Employees e
	WHERE EXISTS (SELECT * FROM dbo.Customers c2 WHERE c2.City = e.City)
UNION SELECT
	c.CompanyName 						 AS Person,
	'Customer'							 AS Type,
	c.City 							     AS City
	FROM dbo.Customers c
	WHERE EXISTS (SELECT * FROM dbo.Employees e2 WHERE e2.City = c.City)
ORDER BY City, Person

/*3.2.5. Найти всех покупателей, которые живут в одном городе. 
В запросе использовать соединение таблицы Customers c собой -
самосоединение. Высветить колонки CustomerID и City. 
Запрос не должен высвечивать дублируемые записи. 
Для проверки написать запрос, который высвечивает города, которые 
встречаются более одного раза в таблице Customers. Это позволит проверить 
правильность запроса.*/

SELECT 
	c.CustomerID 			AS CustomerID,
	c.City 					AS City
FROM dbo.Customers c 
WHERE EXISTS (SELECT * FROM dbo.Customers c2 WHERE c.City = c2.City AND c.CustomerID <> c2.CustomerID)
ORDER BY City 

/*3.2.6. По таблице Employees найти для каждого продавца его руководителя, т.е. 
кому он делает репорты. 
Высветить колонки с именами 'User Name' (LastName) и 'Boss'. В колонках должны 
быть высвечены имена из колонки LastName. 
Высвечены ли все продавцы в этом запросе?*/

SELECT 
	e.LastName 							AS UserName,
	e2.LastName 						AS Boss
FROM dbo.Employees e 
LEFT JOIN dbo.Employees e2 				ON e2.EmployeeID = e.ReportsTo 

--Высвечены все продавцы. Но у 'Fuller' нет босса, т.к. он никому не делает репорты

/*4.1.1.Написать запрос, который добавляет новый заказ в таблицу dbo.Orders
Необходимо написать два запроса
• первый с использованием Values;
• второй с использованием Select*/
INSERT INTO dbo.Orders 
VALUES (N'RATTC',1,'8/2/2021','8/12/2021','8/8/2021',3,50.00,
	N'Avrora',N'5ª St. Petersburg',N'Petersburg',
	N'RU',N'1081',N'Russia')
	
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
VALUES (1,'Война и мир'),
        (2,'Преступление и наказание'),
        (3,'Мастер и Маргарита'),
        (4,'Тихий дон')
 
-- Добавление данных во временную таблицу tblBookInLibrary       
INSERT INTO #tblBookInLibrary
VALUES (1,CAST('2006-05-01' as datetime)),
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
