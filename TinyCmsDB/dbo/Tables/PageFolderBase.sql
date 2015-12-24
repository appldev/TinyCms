CREATE TABLE [dbo].[PageFolderBase] (
    [Id]          UNIQUEIDENTIFIER CONSTRAINT [DF_PageFolder_Id] DEFAULT (newid()) NOT NULL,
    [PageHostId]  UNIQUEIDENTIFIER NOT NULL,
    [ParentId]    UNIQUEIDENTIFIER NULL,
    [Name]        VARCHAR (50)     NOT NULL,
    [FolderIndex] INT              CONSTRAINT [DF_PageFolder_FolderIndex] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PageFolderBase] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PageFolderBase_PageFolderBase] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[PageFolderBase] ([Id])
);




GO
CREATE CLUSTERED INDEX [IX_PageFolderBase_PageHostId]
    ON [dbo].[PageFolderBase]([PageHostId] ASC);

