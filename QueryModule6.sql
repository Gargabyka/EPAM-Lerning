/*6.2. По таблице dbo.Employees для каждого руководителя найти подчиненных 
на всех уровнях иерархии подчинения (напряму и через других 
подчиненных). Вывести руководителя, подчиненного, непосредственного 
руководителя и уровень подчинения. 
Для построения иерархии в таблице используются поля EmploeeID и 
ReportsTo.
Необходимо использовать рекурсивыный CTE.*/

;WITH Boss 
AS
(
	SELECT 
		e.EmployeeID, 
		e.ReportsTo,
		0 as level
	FROM dbo.Employees e
	WHERE e.ReportsTo IS NULL
		UNION ALL
	SELECT
		e.EmployeeID, 
		e.ReportsTo,
		s.level + 1 as level
	FROM Boss s, dbo.Employees e
	WHERE s.EmployeeID = e.ReportsTo
)
SELECT 
	CONCAT(e2.LastName, ' ', e2.FirstName)	AS Boss,
	CONCAT(e.LastName, ' ', e.FirstName)	AS Employee,
	s.level									AS Level,
	CONCAT(e3.LastName, ' ', e3.FirstName)	AS Supervisor
FROM Boss s
JOIN dbo.Employees e	ON e.EmployeeID = s.EmployeeID
JOIN dbo.Employees e2	ON e2.EmployeeID = s.ReportsTo
JOIN dbo.Employees e3	ON e3.EmployeeID = e.ReportsTo
