-- Task 4
SELECT * FROM Departments

-- Task 5
SELECT Name FROM Departments

-- Task 6
SELECT FirstName + ' ' + LastName AS Employee, Salary FROM Employees

-- Task 7
SELECT CONCAT(FirstName, ' ', MiddleName, ' ', LastName)
 AS [Full Name] FROM Employees

-- Task 8
SELECT FirstName + '.' + LastName + '@telerik.com'
 AS [Full Email Addresses] FROM Employees

-- Task 9
SELECT DISTINCT Salary FROM Employees

-- Task 10
SELECT * FROM Employees
 WHERE JobTitle = 'Sales Representative'

 -- Task 11
SELECT FirstName, MiddleName, LastName FROM Employees
 WHERE FirstName LIKE 'SA%'

-- Task 12
SELECT FirstName, MiddleName, LastName FROM Employees
 WHERE LastName LIKE '%ei%'

-- Task 13
SELECT Salary FROM Employees
 WHERE Salary BETWEEN 20000 AND 30000

-- Task 14
SELECT FirstName, LastName, Salary FROM Employees
 WHERE Salary IN(25000, 14000, 12500)

-- Task 15
SELECT FirstName, LastName, ManagerID FROM Employees
 WHERE ManagerID IS NULL

-- Task 16
SELECT FirstName, LastName, Salary FROM Employees
 WHERE Salary > 50000
 ORDER BY Salary DESC

-- Task 17
SELECT TOP 5 FirstName, LastName, Salary FROM Employees
 ORDER BY Salary DESC

-- Task 18 
-- Write a SQL query to find all employees along with their address.
-- Use inner join with ON clause.
SELECT e.FirstName + ' ' + e.LastName AS [Employee Name],
a.AddressText
FROM Employees e
 INNER JOIN Addresses a
  ON e.AddressID = a.AddressID

-- Task 19
-- Write a SQL query to find all employees and their address.
-- Use equijoins (conditions in the WHERE clause).
SELECT e.FirstName + ' ' + e.LastName AS [Employee Name],
a.AddressText
FROM Employees e, Addresses a
 WHERE e.AddressID = a.AddressID
 
-- Task 20
-- Write a SQL query to find all employees along with their manager.
SELECT e.FirstName + ' ' + e.LastName AS [Employee Name],
m.FirstName + ' ' + m.LastName AS [Manager Name]
FROM Employees e, Employees m
 WHERE e.ManagerID = m.EmployeeID

-- Task 21
-- Write a SQL query to find all employees,
-- along with their manager and their address.
-- Join the 3 tables: Employees e, Employees m and Addresses a.
SELECT e.FirstName + ' ' + e.LastName AS [Employee Name],
m.FirstName + ' ' + m.LastName AS [Manager Name], a.AddressText
FROM Employees e
 LEFT OUTER JOIN Employees m
  ON e.ManagerID = m.EmployeeID
 JOIN Addresses a
  ON e.AddressID = a.AddressID

-- Task 22
-- Write a SQL query to find all departments and all town names
-- as a single list. Use UNION.
SELECT Name AS [Departments And Towns Name] FROM Departments
UNION
SELECT Name FROM Towns

-- Task 23
-- Write a SQL query to find all the employees and the manager
-- for each of them along with the employees that do not have manager.
-- Use right outer join.
SELECT e.FirstName + ' ' + e.LastName AS [Employee Name],
m.FirstName + ' ' + m.LastName AS [Manager Name]
FROM Employees m
 RIGHT OUTER JOIN Employees e
  ON e.ManagerID = m.EmployeeID

-- Rewrite the query to use left outer join.
SELECT e.FirstName + ' ' + e.LastName AS [Employee Name],
m.FirstName + ' ' + m.LastName AS [Manager Name]
FROM Employees e
 LEFT OUTER JOIN Employees m
  ON e.ManagerID = m.EmployeeID

-- Task 24
-- Write a SQL query to find the names of all employees
-- from the departments "Sales" and "Finance" whose hire year is between 1995 and 2005.
SELECT e.FirstName + ' ' + e.LastName AS [Employee Name],
e.HireDate, d.Name AS [Department Name]
FROM Employees e
 INNER JOIN Departments d
  ON (e.DepartmentID = d.DepartmentID
   AND d.Name IN ('Sales', 'Finance')
   AND e.HireDate > CAST('1995-01-01 00:00:00.000' AS smalldatetime)
   AND e.HireDate < CAST('2005-12-31 11:59:59.999' AS smalldatetime))