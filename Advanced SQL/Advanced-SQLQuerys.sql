-- Task 1
-- Write a SQL query to find the names and salaries
-- of the employees that take the minimal salary in the company.
-- Use a nested SELECT statement.
select FirstName, LastName, Salary from Employees
where Salary = (select min(Salary) from Employees)

-- Task 2
-- Write a SQL query to find the names and salaries
-- of the employees that have a salary that is up to
-- 10% higher than the minimal salary for the company.
select FirstName, LastName, Salary from Employees
where Salary > (select min(Salary) from Employees)
	and Salary <= (select min(Salary) * 1.1 from Employees)

-- Task 3
-- Write a SQL query to find the full name, salary and
-- department of the employees that take the minimal
-- salary in their department. Use a nested SELECT statement.
select  concat(e.FirstName, ' ', e.MiddleName, ' ', e.LastName) as [Employee Name],
e.Salary, d.Name as [Department Name]
from Employees e, Departments d
where Salary =
	(select min(Salary) from Employees
	 where DepartmentID = e.DepartmentID)

-- Task 4
-- Write a SQL query to find the average salary in the department #1.
select avg(Salary) as [Average Salary], d.DepartmentID
from Employees e
join Departments d
	on (e.DepartmentID = d.DepartmentID
	and e.DepartmentID = 1)
group by d.DepartmentID

-- Task 5
-- Write a SQL query to find the average salary  in the "Sales" department.
select avg(Salary) as [Average Salary], d.Name as [Department Name]
from Employees e
join Departments d
	on (e.DepartmentID = d.DepartmentID
	and d.Name = 'Sales')
group by d.Name

-- Task 6
-- Write a SQL query to find the number of employees in the "Sales" department.
select count(*) as [Employees Count], d.Name as [Department Name]
from Employees e
join Departments d
	on (e.DepartmentID = d.DepartmentID
	and d.Name = 'Sales')
group by d.Name

-- Task 7
-- Write a SQL query to find the number of all employees that have manager.
select count(*) as [Employees Count] 
from Employees
where ManagerID is not null

-- Task 8
-- Write a SQL query to find the number of all employees that have no manager.
select count(*) as [Employees Count] 
from Employees
where ManagerID is null

-- Task 9
-- Write a SQL query to find all departments and the average salary for each of them.
select d.Name as [Department Name], avg(Salary) as [Average Salary]
from Employees e
join Departments d
	on (e.DepartmentID = d.DepartmentID)
group by d.Name

-- Task 10
-- Write a SQL query to find the count of all employees in each department and for each town.
select count(*) as [Employees Count], d.Name as [Department] , t.Name as [Town]
from Employees e
join Departments d
	on (e.DepartmentID = d.DepartmentID)
join Addresses a
	on (e.AddressID = a.AddressID)
join Towns t
	on (a.TownID = t.TownID)
group by d.Name, t.Name

-- Task 11
-- Write a SQL query to find all managers that have exactly 5 employees.
-- Display their first name and last name.
select concat(m.FirstName, ' ', m.LastName) as [Manager Name],
count(e.EmployeeID) as [Employees Count]
from Employees m
join Employees e
	on (e.ManagerID = m.EmployeeID)
group by m.FirstName, m.LastName
having count(e.EmployeeID) = 5

-- Task 12
-- Write a SQL query to find all employees along with their managers.
-- For employees that do not have manager display the value "(no manager)".

-- Task 13
-- Write a SQL query to find the names of all employees whose last name
-- is exactly 5 characters long. Use the built-in LEN(str) function.
