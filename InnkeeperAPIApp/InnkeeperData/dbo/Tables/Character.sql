CREATE TABLE [dbo].[Character] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (50) NOT NULL,
    [CharacterClass] NVARCHAR (50) NOT NULL,
    [Race]           NVARCHAR (50) NOT NULL,
    [Level]          INT           DEFAULT ((1)) NOT NULL,
    [UserId]         INT           NOT NULL,
    [StatsId]        INT           NULL,
    [CreatedDate]    DATETIME2 (7) DEFAULT (getutcdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

