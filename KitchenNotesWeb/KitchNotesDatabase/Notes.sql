CREATE TABLE [dbo].[Notes]
(
	[NoteId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Note] NVARCHAR(MAX) NOT NULL, 
    [DateAdded] DATETIME NOT NULL, 
    [UserFamilyId] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [fk_UserFamilyId] FOREIGN KEY ([UserFamilyId]) REFERENCES [UserFamily]([UserFamilyId])
)
