CREATE TABLE [dbo].[User] (
    [Id]          NVARCHAR (128) NOT NULL PRIMARY KEY,
    [FirstName]   NVARCHAR (50)  NOT NULL,
    [LastName]    NVARCHAR (50)  NOT NULL,
    [CreatedDate] DATETIME2 (7)  DEFAULT (getutcdate()) NOT NULL
    --PRIMARY KEY CLUSTERED ([Id] ASC)
);

