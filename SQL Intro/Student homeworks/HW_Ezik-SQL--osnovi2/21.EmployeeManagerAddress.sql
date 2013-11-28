/* 21. Write a SQL query to find all employees, along with their manager and their address. Join the 3 tables: Employees e, Employees m and Addresses a. */
SELECT e.FirstName + ' ' + e.LastName AS EmployeeName, 
	m.FirstName + ' ' + m.LastName AS ManagerName, 
	a.AddressText
FROM Employees e, Employees m, Addresses a
WHERE e.ManagerID = m.EmployeeID
	AND e.AddressID = a.AddressID
