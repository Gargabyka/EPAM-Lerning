/*9.1. Написать скрипт, который обновит в поле PostalCode таблицы 
dbo.Employees все не числоввые символы на любые числовые*/

UPDATE dbo.Employees SET PostalCode = CAST(100000 + (RAND(CHECKSUM(NEWID())) * 999999) as INT) 
FROM dbo.Employees 
WHERE PostalCode LIKE '%[^0-9]%'