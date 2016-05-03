﻿/*
Deployment script for KitchNotesDatabase

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "KitchNotesDatabase"
:setvar DefaultFilePrefix "KitchNotesDatabase"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL12.AMEH2015\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL12.AMEH2015\MSSQL\DATA\"

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
USE [$(DatabaseName)];


GO
PRINT N'Rename refactoring operation with key c107de4c-6b9d-4a0f-8779-60f97a9b88ea is skipped, element [dbo].[Notes].[dateHidden] (SqlSimpleColumn) will not be renamed to DateHidden';


GO
PRINT N'Altering [dbo].[Hub]...';


GO
ALTER TABLE [dbo].[Hub]
    ADD [HubAddress]  NCHAR (100) NULL,
        [HubLocation] NCHAR (20)  NULL;


GO
PRINT N'Altering [dbo].[Notes]...';


GO
ALTER TABLE [dbo].[Notes]
    ADD [DateEdited] DATETIME NULL,
        [DateHidden] DATETIME NULL;


GO
-- Refactoring step to update target server with deployed transaction logs
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'c107de4c-6b9d-4a0f-8779-60f97a9b88ea')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('c107de4c-6b9d-4a0f-8779-60f97a9b88ea')

GO

GO
PRINT N'Update complete.';


GO
