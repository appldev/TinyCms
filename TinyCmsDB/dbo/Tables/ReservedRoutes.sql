CREATE TABLE [dbo].[ReservedRoutes] (
    [Name]        VARCHAR (20)  NOT NULL,
    [RouteOrder]  INT           CONSTRAINT [DF_ReservedRoutes_RouteOrder] DEFAULT ((0)) NOT NULL,
    [RoutePath]   VARCHAR (50)  NOT NULL,
    [Defaults]    VARCHAR (200) NULL,
    [Constraints] VARCHAR (200) NULL,
    [Namespaces]  VARCHAR (200) NULL,
    [IsSystem]    BIT           CONSTRAINT [DF_ReservedRoutes_IsSystem] DEFAULT ((0)) NOT NULL,
    [IsIgnore]    BIT           CONSTRAINT [DF_ReservedRoutes_IsIgnore] DEFAULT ((0)) NOT NULL,
    [IsActive]    BIT           CONSTRAINT [DF_ReservedRoutes_IsActive] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ReservedRoutes] PRIMARY KEY CLUSTERED ([Name] ASC)
);




