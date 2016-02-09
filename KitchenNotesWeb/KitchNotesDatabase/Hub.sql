CREATE TABLE [dbo].[Hub]
(
	[HubId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [HubName] NCHAR(20) NULL, 
    [HubAddress] NCHAR(100) NULL, 
    [HubLocation] NCHAR(20) NULL
)
