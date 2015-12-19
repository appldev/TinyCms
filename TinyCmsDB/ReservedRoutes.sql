CREATE TABLE [dbo].[ReservedRoutes]
(
	[RoutePath] VARCHAR(50) NOT NULL , 
    [Controller] VARCHAR(50) NOT NULL, 
    [Action] VARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_ReservedRoutes] PRIMARY KEY ([RoutePath])
)
