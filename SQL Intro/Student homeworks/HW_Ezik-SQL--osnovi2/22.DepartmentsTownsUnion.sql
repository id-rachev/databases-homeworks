/* 22. Write a SQL query to find all departments and all town names as a single list. Use UNION. */

SELECT d.Name FROM Departments d
UNION
SELECT t.Name FROM Towns t