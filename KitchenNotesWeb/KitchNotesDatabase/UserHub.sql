﻿CREATE TABLE [dbo].[UserHub]
(
	[UserHubId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [HubId] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [fk_UserId] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]), 
    CONSTRAINT [fk_HubId] FOREIGN KEY ([HubId]) REFERENCES [Hub]([HubId])
)
