/*9.1. Написать скрипт, который обновит в поле PostalCode таблицы 
dbo.Employees все не числоввые символы на любые числовые*/

UPDATE dbo.Employees SET PostalCode = CAST(100000 + (RAND(CHECKSUM(NEWID())) * 999999) as INT) 
FROM dbo.Employees 
WHERE PostalCode LIKE '%[^0-9]%'

/*9.2. Построить план и оптимизировать запрос, представленный ниже, так 
чтобы индекс индекс PostalCode работал не по табличному сканированию 
(Index Scan), а по Index Seek. Необходимо пояснить, почему вы 
оптимизировали запрос именно так*/

SELECT EmployeeID
FROM dbo.Employees
WHERE PostalCode LIKE '98%';

/*9.3. Разобраться с планом запроса, представленного ниже скрипта. 
Оптимизировать запрос. Пояснить подробно почему вы считаете, что ваш 
вариант оптимизации наиболее оптимизирует данный запрос и увеличит 
его быстродействие?*/

CREATE NONCLUSTERED COLUMNSTORE INDEX Orders_OrderDate_Index 
ON dbo.Orders(OrderDate,ShippedDate)