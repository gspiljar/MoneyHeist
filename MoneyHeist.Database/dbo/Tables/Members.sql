﻿CREATE TABLE [dbo].[Members]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Sex] CHAR NOT NULL, 
    [Email] NVARCHAR(100) NOT NULL, 
    [MainSkill] NVARCHAR(100) NOT NULL, 
    [Status] VARCHAR(12) NOT NULL
)
