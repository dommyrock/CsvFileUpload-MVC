CREATE DATABASE CsvDb

CREATE TABLE [dbo].[Podaci](
	[Id] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Podaci] PRIMARY KEY,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[ZipCode] [nvarchar](100) NOT NULL,
	[City] [nvarchar](120) NOT NULL,
	[Phone] [nvarchar](200) NULL
)
GO

IF OBJECT_ID ( 'Proc_InsertData', 'P' ) IS NOT NULL   
    DROP PROCEDURE Proc_InsertData;  
GO
--Procedure disables duplicateas and if gets one ... Outputs server log 
CREATE PROCEDURE [dbo].[Proc_InsertData]
@FirstName nvarchar(100),
@LastName nvarchar(100),
@ZipCode nvarchar(5),
@City nvarchar(120),
@Phone nvarchar(100)

AS
IF NOT EXISTS(SELECT FirstName FROM Podaci WHERE FirstName=@FirstName AND LastName=@LastName)
BEGIN
	INSERT INTO Podaci
	(FirstName,LastName,ZipCode,City,Phone)
	VALUES (@FirstName,@LastName,@ZipCode,@City,@Phone)
END  

ELSE 
BEGIN
--return server log if not unique-
			 
	 RAISERROR ('User already exists in DB',-- Message text.  
               16, -- Severity.  
               1 -- State.  
               )WITH LOG; --log to server log
END

--test insert
EXEC Proc_InsertData @FirstName ='Dommy', @LastName ='Polzer', @ZipCode= '12345',@City ='zagreb',@Phone ='123-123'
EXEC Proc_InsertData @FirstName ='Dommy', @LastName ='Polzer', @ZipCode= '12345',@City ='ZAgreb',@Phone ='123-123'
-- PRINT N'text'  ---> print to console (for test)
