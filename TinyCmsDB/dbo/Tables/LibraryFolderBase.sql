CREATE TABLE [dbo].[LibraryFolderBase] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        VARCHAR (100)    NOT NULL,
    [Title]       VARCHAR (100)    NULL,
    [Description] VARCHAR (400)    NULL,
    [ParentId]    UNIQUEIDENTIFIER NULL,
    [LibraryId]   UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_LibraryFolder] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LibraryFolder_LibraryBase] FOREIGN KEY ([LibraryId]) REFERENCES [dbo].[LibraryBase] ([Id]),
    CONSTRAINT [FK_LibraryFolder_LibraryFolder] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[LibraryFolderBase] ([Id])
);

