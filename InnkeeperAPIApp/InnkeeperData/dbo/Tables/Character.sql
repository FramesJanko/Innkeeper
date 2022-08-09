CREATE TABLE [dbo].[Character]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [CharacterClass] NVARCHAR(50) NOT NULL, 
    [Race] NVARCHAR(50) NOT NULL, 
    [Level] INT NOT NULL DEFAULT 1, 
    [UserId] INT NOT NULL, 
    [StatsId] INT NULL, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate()
)
