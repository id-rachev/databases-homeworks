/* 24. Write a SQL query to find the names of all employees from the departments "Sales" and "Finance" whose hire year is between 1995 and 2005. */

SELECT e.FirstName AS FirstName, 
	DATEPART(YEAR, e.HireDate) AS HireYear,
	d.Name AS DeparatmentName
FROM Employees e 
JOIN Departments d
ON e.DepartmentID = d.DepartmentID
WHERE d.Name = 'Sales' 
OR d.Name = 'Finance'
AND DATEPART(YEAR, e.HireDate) >= 1995
AND DATEPART(YEAR, e.HireDate) <= 2005