/* 20. Write a SQL query to find all employees along with their manager. */
SELECT e.*, m.FirstName + ' ' + m.LastName AS ManagerName
FROM Employees e, Employees m
WHERE e.ManagerID = m.EmployeeID