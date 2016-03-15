CREATE TABLE [dbo].[Tasks]
(
	[TaskId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [TaskDetail] NCHAR(200) NOT NULL, 
    [AssignedTo] NCHAR(50) NULL, 
    [UserHubId] UNIQUEIDENTIFIER NOT NULL, 
    [Completed] BIT NOT NULL, 
    [Hidden] BIT NOT NULL, 
    [DatePosted] DATETIME NOT NULL, 
    CONSTRAINT [UserHubId] FOREIGN KEY ([UserHubId]) REFERENCES [UserHub]([UserHubId])
)
