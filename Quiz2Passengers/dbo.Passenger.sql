CREATE TABLE [dbo].[Passenger]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(100) NULL, 
    [PassportNo] VARCHAR(10) NULL, 
    [Destination] VARCHAR(100) NULL, 
    [DepartureDateTime] DATETIME NOT NULL
)
