CREATE TABLE [dbo].[Notes]
(
	[NoteId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Note] NVARCHAR(MAX) NOT NULL, 
    [DateAdded] DATETIME NOT NULL, 
    [UserHubId] UNIQUEIDENTIFIER NULL, 
    [DateEdited] DATETIME NULL, 
    [DateHidden] DATETIME NULL, 
    CONSTRAINT [fk_UserHubId] FOREIGN KEY ([UserHubId]) REFERENCES [UserHub]([UserHubId])
)
