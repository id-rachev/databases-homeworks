--Task 1
CREATE Database BankAccount
GO

USE BankAccount
CREATE TABLE Persons (
  PersonID int identity,
  FirstName nvarchar(100) not null,
  LastName nvarchar(100) not null,
  SSN int not null
  constraint PK_PersonID primary key(PersonID),
)
GO
CREATE TABLE Accounts (
  AccountID int identity,
  Balance money DEFAULT 0,
  PersonId int null
  CONSTRAINT PK_AccountID primary key(AccountID),
  CONSTRAINT FK_Accounts_Persons
  FOREIGN KEY (PersonId)
  REFERENCES Persons(PersonId)
)
GO

INSERT INTO Persons(FirstName, LastName, SSN)
VALUES ('Milcho', 'Ivanov', 41223234)
INSERT INTO Persons(FirstName, LastName, SSN)
VALUES ('Penyo', 'Todorov', 42342345)
INSERT INTO Persons(FirstName, LastName, SSN)
VALUES ('Angel', 'Petkov', 36346456)
INSERT INTO Persons(FirstName, LastName, SSN)
VALUES ('Milka', 'Muncheva', 3123123)
INSERT INTO Accounts(Balance, PersonId)
VALUES (200, 2)
INSERT INTO Accounts(Balance, PersonId)
VALUES (300, 3)
INSERT INTO Accounts(Balance, PersonId)
VALUES (400, 4)
GO

CREATE PROC dbo.usp_SelectFullNames
AS
  SELECT  concat(FirstName, ' ', LastName) AS [Full Name]
  FROM Persons
GO

EXEC usp_SelectFullNames
GO
--Task 2

CREATE PROC dbo.usp_SelectPersonMoreMoney (@moneybalance money)
AS
  SELECT  concat(FirstName, ' ', LastName) AS [Full Name], acc.Balance
  FROM Persons p
  JOIN Accounts acc
  ON acc.PersonId = p.PersonID
  WHERE acc.Balance > @moneybalance
  ORDER BY acc.Balance
GO

--DROP PROC dbo.usp_SelectPersonMoreMoney

EXEC usp_SelectPersonMoreMoney 200
GO
--Task 3
CREATE FUNCTION ufn_CalcInterest(@sum money, @yearlyInterest numeric(18, 2), @months int)
  RETURNS numeric(18, 2)
AS
BEGIN
  RETURN (@yearlyInterest / 12) * @months * @sum + @sum
END
GO

--drop function ufn_CalcInterest

select dbo.ufn_CalcInterest(200, 0.2, 5)
GO
--Task 4
CREATE PROC dbo.usp_CalculateInterest (@accountID int, @yearlyInterest numeric(18, 2))
AS
 UPDATE Accounts
 SET Balance = CONVERT (money,dbo.ufn_CalcInterest(Balance, @yearlyInterest, 1))
 WHERE AccountID = @accountID
GO

EXEC dbo.usp_CalculateInterest 2,0.2
GO

--Task 5
CREATE PROC dbo.usp_WithdrawMoney (@accountID int, @money money)
AS
BEGIN TRAN
 UPDATE Accounts
 SET Balance = Balance - @money
 WHERE AccountID = @accountID
COMMIT TRAN
GO

--drop proc dbo.usp_WithdrawMoney

CREATE PROC dbo.usp_DepositMoney (@accountID int, @money money)
AS
BEGIN TRAN
 UPDATE Accounts
 SET Balance = Balance + @money
 WHERE AccountID = @accountID
 COMMIT TRAN
GO

EXEC dbo.usp_WithdrawMoney 2, 50
EXEC dbo.usp_WithdrawMoney 1, 50
EXEC dbo.usp_DepositMoney 1, 200
GO

--Task 6
CREATE TABLE Logs (
  LogID int identity,
  OldSum money not null,
  NewSum money not null,
  AccountID int not null,
  CONSTRAINT PK_LogID primary key(LogID),
  CONSTRAINT FK_Logs_Accounts
  FOREIGN KEY (AccountID)
  REFERENCES Accounts(AccountID)
)
GO

CREATE TRIGGER tr_AccountsUpdate ON Accounts FOR UPDATE
AS

INSERT INTO Logs (OldSum, NewSum, AccountID)
SELECT d.Balance,
	   i.Balance,
	   d.AccountID
  FROM deleted AS d
  JOIN inserted AS i
    ON d.AccountID = i.AccountID
GO 

EXEC dbo.usp_DepositMoney 1, 200
GO

--Task 7 (regularExpresions for SQL http://www.codeproject.com/Articles/42764/Regular-Expressions-in-MS-SQL-Server-2005-2008)
sp_configure 'clr enabled', 1
GO
RECONFIGURE
GO

USE TelerikAcademy
CREATE ASSEMBLY 
--assembly name for references from SQL script
SqlRegularExpressions 
-- assembly name and full path to assembly dll,
-- SqlRegularExpressions in this case
from 'D:\4. T-SQL\SqlRegularExpressions.dll'  --change path to dll
WITH PERMISSION_SET = SAFE
GO
--function signature
CREATE FUNCTION RegExpLike(@Text nvarchar(max), @Pattern nvarchar(255)) RETURNS BIT
--function external name
AS EXTERNAL NAME SqlRegularExpressions.SqlRegularExpressions.[Like]

GO

CREATE FUNCTION fn_RegularExpressionFind ( @regularExpression nvarchar(30) )
RETURNS TABLE
AS
return SELECT Emp.FirstName,
	   Emp.MiddleName,
	   Emp.LastName,
	   Towns.Name
  FROM Employees AS Emp
  JOIN Addresses As Addr
    ON Emp.AddressID = Addr.AddressID
  JOIN Towns
    ON Addr.TownID = Towns.TownID
 WHERE 1 = dbo.RegExpLike(LOWER(Towns.Name), @regularExpression)
   AND (1 = dbo.RegExpLike(LOWER(Emp.FirstName), @regularExpression)
    OR 1 = dbo.RegExpLike(LOWER(ISNULL(Emp.MiddleName, '')), @regularExpression)
	OR 1 = dbo.RegExpLike(LOWER(Emp.LastName), @regularExpression))
GO

select * from fn_RegularExpressionFind('^[oistmiahf]+$')