CREATE TABLE [dbo].[LibraryType] (
    [Id]        INT           NOT NULL,
    [Name]      VARCHAR (10)  NOT NULL,
    [FileTypes] VARCHAR (200) NULL,
    CONSTRAINT [PK_LibraryType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

