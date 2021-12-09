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