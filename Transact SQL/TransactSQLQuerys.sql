-- Task 1
-- Create a database with two tables: Persons(Id(PK), FirstName, LastName, SSN)
-- and Accounts(Id(PK), PersonId(FK), Balance). Insert few records for testing.
-- Write a stored procedure that selects the full names of all persons.

create database BankAccount
go

use BankAccount
create table Persons (
	PersonId int identity,
	FirstName nvarchar(50) not null,
	LastName nvarchar(50) not null,
	SSN int
	constraint PK_PersonId primary key (PersonId)
)
go
create table Accounts (
	AccountId int identity,
	PersonId int not null,
	Balance money default 0
	constraint PK_AccountId primary key (AccountId)
	constraint FK_Accounts_Persons
	foreign key (PersonId)
	references Persons(PersonId)
)
go

insert into Persons (FirstName, LastName, SSN)
values ('Pesho', 'Peshev', 12351)
insert into Persons (FirstName, LastName, SSN)
values ('Gosho', 'Goshov', 13351)
insert into Persons (FirstName, LastName, SSN)
values ('Mosho', 'Moshov', 11351)
go
insert into Accounts(PersonId, Balance)
values (1, 25)
insert into Accounts(PersonId, Balance)
values (2, 254)
insert into Accounts(PersonId, Balance)
values (3, 1232)
go

create proc dbo.usp_SelectFullNames
as 
select concat(FirstName, ' ', LastName) as [Full Name]
from Persons
go

exec dbo.usp_SelectFullNames
go

-- Task 2
-- Create a stored procedure that accepts a number as a parameter
-- and returns all persons who have more money in their accounts
-- than the supplied number.
create proc dbo.usp_SelectPersonsWithBalanceAbove(
	@balancePivot int = 0)
as
	select concat(p.FirstName, ' ', p.LastName) as [Full Name], a.Balance
	from Persons p
	join Accounts a
	on a.PersonId = p.PersonId
	and a.Balance > @balancePivot
go

exec dbo.usp_SelectPersonsWithBalanceAbove 100
go

-- Task 3
-- Create a function that accepts as parameters – sum, yearly interest
-- rate and number of months. It should calculate and return the new sum.
-- Write a SELECT to test whether the function works as expected.
create function dbo.ufn_CalcYearlyBalance(
	@sum money, @yearlyInterest numeric(18, 2), @months int)
	returns numeric(18, 2)
as
begin
	return (@yearlyInterest / 12) * @months * @sum + @sum
end
go

-- drop function dbo.ufn_CalcYearlyBalance
select dbo.ufn_CalcYearlyBalance(100, 0.04, 12)
go

-- Task 4
-- Create a stored procedure that uses the function from the previous
-- example to give an interest to a person's account for one month.
-- It should take the AccountId and the interest rate as parameters.
create proc dbo.usp_CalcInterest (@accountId int, @yearlyInterest numeric(18, 2))
as
	update Accounts
	set Balance = convert (money, dbo.ufn_CalcYearlyBalance(Balance, @yearlyInterest, 1))
	where AccountId = @accountId
go

-- drop proc dbo.usp_CalcInterest
exec dbo.usp_CalcInterest 1, 0.4
go
exec dbo.usp_SelectPersonsWithBalanceAbove 0
go

-- Task 5
-- Add two more stored procedures WithdrawMoney( AccountId, money)
-- and DepositMoney (AccountId, money) that operate in transactions.
create proc dbo.usp_WithdrawMoney (@accountId int, @money money)
as
	begin tran
		update Accounts
		set Balance = Balance - @money
		where AccountId = @accountId
	commit tran
go

create proc dbo.usp_DepositMoney (@accountId int, @money money)
as
	begin tran
		update Accounts
		set Balance = Balance + @money
		where AccountId = @accountId
	commit tran
go

exec dbo.usp_WithdrawMoney 1, 5
exec dbo.usp_DepositMoney 2, 6
go
exec dbo.usp_SelectPersonsWithBalanceAbove 0
go

-- Task 6
-- Create another table – Logs(LogID, AccountID, OldSum, NewSum).
-- Add a trigger to the Accounts table that enters a new entry
-- into the Logs table every time the sum on an account changes.

use BankAccount
create table Logs (
	LogId int identity,
	AccountId int not null,
	OldSum money default 0,
	NewSum money default 0
	constraint PK_LogId primary key (LogId)
	constraint FK_Logs_Accounts
	foreign key (AccountId)
	references Accounts(AccountId)
)
go

create trigger tr_AccountsUpdate on Accounts for update
as
	insert into Logs (AccountId, OldSum, NewSum)
	select d.AccountId,
		d.Balance,
		i.Balance
	from deleted as d
	join inserted as i
	on d.AccountId = i.AccountId
go

-- drop trigger tr_AccountsUpdate
exec dbo.usp_DepositMoney 1, 5
go
select * from Logs
go

-- Task 7
-- Define a function in the database TelerikAcademy that returns all
-- Employee's names (first or middle or last name) and all town's names
-- that are comprised of given set of letters. Example 'oistmiahf' will
-- return 'Sofia', 'Smith', … but not 'Rob' and 'Guy'.
