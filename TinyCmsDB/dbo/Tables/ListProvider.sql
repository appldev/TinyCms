CREATE TABLE [dbo].[ListProvider] (
    [Id]   UNIQUEIDENTIFIER CONSTRAINT [DF_ListProvider_Id] DEFAULT (newid()) NOT NULL,
    [Type] VARCHAR (200)    NOT NULL,
    [Name] VARCHAR (200)    NOT NULL,
    CONSTRAINT [PK_ListProvider] PRIMARY KEY CLUSTERED ([Id] ASC)
);

