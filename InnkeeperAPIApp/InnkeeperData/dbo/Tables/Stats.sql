CREATE TABLE [dbo].[Stats]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Strength] INT NOT NULL, 
    [Dexterity] INT NOT NULL, 
    [Constitution] INT NOT NULL, 
    [Intelligence] INT NOT NULL, 
    [Wisdom] INT NOT NULL, 
    [Charisma] INT NOT NULL, 
    [Health] INT NOT NULL, 
    [ArmorClass] INT NOT NULL, 
    [Speed] INT NOT NULL
)
