/* 18. Write a SQL query to find all employees along with their address. Use inner join with ON clause. */
SELECT e.*, ad.AddressText AS AddressText
FROM Employees e JOIN Addresses ad
ON e.AddressID = ad.AddressID
