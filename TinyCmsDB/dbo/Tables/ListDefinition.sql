CREATE TABLE [dbo].[ListDefinition] (
    [Id]   UNIQUEIDENTIFIER CONSTRAINT [DF_ListDefinition_Id] DEFAULT (newid()) NOT NULL,
    [Name] VARCHAR (200)    NOT NULL,
    CONSTRAINT [PK_ListDefinition] PRIMARY KEY CLUSTERED ([Id] ASC)
);

