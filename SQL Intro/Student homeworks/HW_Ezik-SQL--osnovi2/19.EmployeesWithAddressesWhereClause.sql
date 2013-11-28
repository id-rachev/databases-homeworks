/* 19. Write a SQL query to find all employees and their address. Use equijoins (conditions in the WHERE clause). */
SELECT e.*, ad.AddressText AS AddressText
FROM Employees e, Addresses ad
WHERE e.AddressID = ad.AddressID