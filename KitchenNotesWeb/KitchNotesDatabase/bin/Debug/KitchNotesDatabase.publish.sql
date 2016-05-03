﻿/*
Deployment script for KitchNotesDB

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "KitchNotesDB"
:setvar DefaultFilePrefix "KitchNotesDB"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET PAGE_VERIFY NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Rename refactoring operation with key 008ba539-92ac-4096-bcdc-fea6cf3efa58 is skipped, element [dbo].[Family].[Id] (SqlSimpleColumn) will not be renamed to FamilyId';


GO
PRINT N'Rename refactoring operation with key a33abac9-3c2c-47f4-b525-bdec85c6c5ec is skipped, element [dbo].[Notes].[Id] (SqlSimpleColumn) will not be renamed to NoteId';


GO
PRINT N'Rename refactoring operation with key f00f73e7-ff27-4d2d-911f-17324c868551 is skipped, element [dbo].[UserFamily].[Id] (SqlSimpleColumn) will not be renamed to UserFamilyId';


GO
PRINT N'Rename refactoring operation with key 03c4ddfc-b3ca-45ab-a643-b0ff14c34f71 is skipped, element [dbo].[User].[Id] (SqlSimpleColumn) will not be renamed to UserId';


GO
PRINT N'Rename refactoring operation with key 9f0f18b3-fcaa-407b-8979-251adbcfbcc1 is skipped, element [dbo].[User].[Forename] (SqlSimpleColumn) will not be renamed to Username';


GO
PRINT N'Rename refactoring operation with key 74b361b1-b54e-452f-9fb0-d5a1fedd000b is skipped, element [dbo].[UserFamilyId] (SqlForeignKeyConstraint) will not be renamed to [fk_UserFamilyId]';


GO
PRINT N'Creating [dbo].[Family]...';


GO
CREATE TABLE [dbo].[Family] (
    [FamilyId]   UNIQUEIDENTIFIER NOT NULL,
    [FamilyName] NCHAR (10)       NULL,
    PRIMARY KEY CLUSTERED ([FamilyId] ASC)
);


GO
PRINT N'Creating [dbo].[Notes]...';


GO
CREATE TABLE [dbo].[Notes] (
    [NoteId]       UNIQUEIDENTIFIER NOT NULL,
    [Note]         NVARCHAR (MAX)   NOT NULL,
    [DateAdded]    DATETIME         NOT NULL,
    [UserFamilyId] UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([NoteId] ASC)
);


GO
PRINT N'Creating [dbo].[User]...';


GO
CREATE TABLE [dbo].[User] (
    [UserId]   UNIQUEIDENTIFIER NOT NULL,
    [Username] NVARCHAR (50)    NOT NULL,
    [Forename] NVARCHAR (50)    NOT NULL,
    [Password] NCHAR (10)       NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);


GO
PRINT N'Creating [dbo].[UserFamily]...';


GO
CREATE TABLE [dbo].[UserFamily] (
    [UserFamilyId] UNIQUEIDENTIFIER NOT NULL,
    [UserId]       UNIQUEIDENTIFIER NOT NULL,
    [FamilyId]     UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([UserFamilyId] ASC)
);


GO
PRINT N'Creating [dbo].[fk_UserFamilyId]...';


GO
ALTER TABLE [dbo].[Notes] WITH NOCHECK
    ADD CONSTRAINT [fk_UserFamilyId] FOREIGN KEY ([UserFamilyId]) REFERENCES [dbo].[UserFamily] ([UserFamilyId]);


GO
PRINT N'Creating [dbo].[fk_UserId]...';


GO
ALTER TABLE [dbo].[UserFamily] WITH NOCHECK
    ADD CONSTRAINT [fk_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]);


GO
PRINT N'Creating [dbo].[fk_FamilyId]...';


GO
ALTER TABLE [dbo].[UserFamily] WITH NOCHECK
    ADD CONSTRAINT [fk_FamilyId] FOREIGN KEY ([FamilyId]) REFERENCES [dbo].[Family] ([FamilyId]);


GO
-- Refactoring step to update target server with deployed transaction logs

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '008ba539-92ac-4096-bcdc-fea6cf3efa58')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('008ba539-92ac-4096-bcdc-fea6cf3efa58')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'a33abac9-3c2c-47f4-b525-bdec85c6c5ec')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('a33abac9-3c2c-47f4-b525-bdec85c6c5ec')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'f00f73e7-ff27-4d2d-911f-17324c868551')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('f00f73e7-ff27-4d2d-911f-17324c868551')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '03c4ddfc-b3ca-45ab-a643-b0ff14c34f71')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('03c4ddfc-b3ca-45ab-a643-b0ff14c34f71')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '9f0f18b3-fcaa-407b-8979-251adbcfbcc1')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('9f0f18b3-fcaa-407b-8979-251adbcfbcc1')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '74b361b1-b54e-452f-9fb0-d5a1fedd000b')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('74b361b1-b54e-452f-9fb0-d5a1fedd000b')

GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Notes] WITH CHECK CHECK CONSTRAINT [fk_UserFamilyId];

ALTER TABLE [dbo].[UserFamily] WITH CHECK CHECK CONSTRAINT [fk_UserId];

ALTER TABLE [dbo].[UserFamily] WITH CHECK CHECK CONSTRAINT [fk_FamilyId];


GO
PRINT N'Update complete.';


GO