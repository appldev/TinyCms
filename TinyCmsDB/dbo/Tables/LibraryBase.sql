CREATE TABLE [dbo].[LibraryBase] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [Name]          VARCHAR (100)    NOT NULL,
    [Description]   VARCHAR (400)    NULL,
    [LibraryTypeId] INT              NOT NULL,
    [PathRoot]      VARCHAR (100)    NULL,
    CONSTRAINT [PK_Library] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LibraryBase_LibraryType] FOREIGN KEY ([LibraryTypeId]) REFERENCES [dbo].[LibraryType] ([Id])
);

