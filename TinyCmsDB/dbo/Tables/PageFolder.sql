CREATE TABLE [dbo].[PageFolder] (
    [Id]          UNIQUEIDENTIFIER CONSTRAINT [DF_PageFolder_Id] DEFAULT (newid()) NOT NULL,
    [PageHostId]  UNIQUEIDENTIFIER NOT NULL,
    [ParentId]    UNIQUEIDENTIFIER NULL,
    [Name]        VARCHAR (50)     NOT NULL,
    [FolderIndex] INT              CONSTRAINT [DF_PageFolder_FolderIndex] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PageFolder] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PageFolder_PageFolder] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[PageFolder] ([Id])
);

