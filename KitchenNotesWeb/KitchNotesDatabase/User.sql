CREATE TABLE [dbo].[User]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Username] NVARCHAR(50) NOT NULL, 
    [Forename] NVARCHAR(50) NOT NULL, 
	[Surname] NVARCHAR(50) NOT NULL, 
    [Password] NCHAR(40) NOT NULL, 
    [DOB] DATE NOT NULL, 
    [Email] NCHAR(30) NOT NULL, 
    [CurrentHub] UNIQUEIDENTIFIER NOT NULL, 
    [LastLogin] DATETIME NOT NULL
)
