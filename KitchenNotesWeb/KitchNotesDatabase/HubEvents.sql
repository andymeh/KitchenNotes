CREATE TABLE [dbo].[HubEvents]
(
	[HubEventId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
	[UserHubId] UNIQUEIDENTIFIER NOT NULL,
    [Name] NCHAR(20) NOT NULL, 
    [Description] NCHAR(300) NULL, 
    [StartDate] DATETIME NOT NULL, 
    [EndDate] DATETIME NOT NULL, 
    [Importance] NCHAR(20) NULL, 
    CONSTRAINT [FK_HubEvents_UserHub] FOREIGN KEY ([UserHubId]) REFERENCES [UserHub]([UserHubId])
)
