CREATE TABLE [dbo].[DataTypeScript] (
    [DataTypeBaseId]    UNIQUEIDENTIFIER NOT NULL,
    [LibraryItemBaseId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_DataTypeScript] PRIMARY KEY CLUSTERED ([DataTypeBaseId] ASC, [LibraryItemBaseId] ASC),
    CONSTRAINT [FK_DataTypeScript_DataTypeBase] FOREIGN KEY ([DataTypeBaseId]) REFERENCES [dbo].[DataTypeBase] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_DataTypeScript_LibraryItemBase] FOREIGN KEY ([LibraryItemBaseId]) REFERENCES [dbo].[LibraryItemBase] ([Id])
);

