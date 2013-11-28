USE master
GO

CREATE DATABASE DBPerformanceTest
GO

USE DBPerformanceTest
GO

CREATE TABLE Logs(
  LogId int NOT NULL IDENTITY,
  LogText nvarchar(300),
  LogDate datetime,
  CONSTRAINT PK_Logs_LogId PRIMARY KEY (LogId)
)

SET NOCOUNT ON
DECLARE @RowCount int = 10000
WHILE @RowCount > 0
BEGIN
  DECLARE @Text nvarchar(100) = 
    'Log ' + CONVERT(nvarchar(100), @RowCount) + ': ' +
    CONVERT(nvarchar(100), newid())
  DECLARE @Date datetime = 
	DATEADD(month, CONVERT(varbinary, newid()) % (50 * 12), getdate())
  INSERT INTO Logs(LogText, LogDate)
  VALUES(@Text, @Date)
  SET @RowCount = @RowCount - 1
END
SET NOCOUNT OFF

WHILE (SELECT COUNT(*) FROM Logs) < 10000000
BEGIN
  INSERT INTO Logs(LogText, LogDate)
  SELECT LogText, LogDate FROM Logs
END

-------------------------------------------------------------------------------------------------
-- Task 1.
-- Search in the table by date range. Check the speed (without caching).

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT * FROM Logs
WHERE LogDate > '31-Dec-2010' and LogDate < '1-Jan-2012'

-------------------------------------------------------------------------------------------------
-- Task 2.
-- Add an index to speed-up the search by date. Test the search speed (after cleaning the cache).

CREATE INDEX IDX_Logs_LogDate ON Logs(LogDate)

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT * FROM Logs
WHERE LogDate > '31-Dec-2010' and LogDate < '1-Jan-2012'

DROP INDEX IDX_Logs_LogDate ON Logs

-------------------------------------------------------------------------------------------------
-- Task 3.
-- Add a full text index for the text column.
-- Try to search with and without the full-text index and compare the speed.

CREATE FULLTEXT CATALOG LogsFullTextCatalog
WITH ACCENT_SENSITIVITY = OFF

CREATE FULLTEXT INDEX ON Logs(LogText)
KEY INDEX PK_Logs_LogId
ON LogsFullTextCatalog
WITH CHANGE_TRACKING AUTO

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT * FROM Logs
WHERE CONTAINS(LogText, '123')

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT COUNT(*) FROM Logs
WHERE LogText LIKE '%123%'

DROP FULLTEXT INDEX ON Logs
DROP FULLTEXT CATALOG LogsFullTextCatalog