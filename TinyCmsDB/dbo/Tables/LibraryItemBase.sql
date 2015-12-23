CREATE TABLE [dbo].[LibraryItemBase] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [Name]            VARCHAR (200)    NOT NULL,
    [Title]           VARCHAR (200)    NOT NULL,
    [Description]     VARCHAR (400)    NULL,
    [LibraryFolderId] UNIQUEIDENTIFIER NOT NULL,
    [Data]            VARCHAR (MAX)    NULL,
    [CreatedOn]       DATETIME         CONSTRAINT [DF_LibraryItemBase_CreatedOn] DEFAULT (getdate()) NOT NULL,
    [ModifiedOn]      DATETIME         CONSTRAINT [DF_LibraryItemBase_ModifiedOn] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_LibraryItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LibraryItem_LibraryFolder] FOREIGN KEY ([LibraryFolderId]) REFERENCES [dbo].[LibraryFolderBase] ([Id])
);

