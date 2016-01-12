CREATE TABLE [dbo].[EditorType] (
    [Id]           INT           NOT NULL,
    [Name]         VARCHAR (10)  NOT NULL,
    [RenderEditor] VARCHAR (400) NULL,
    CONSTRAINT [PK_EditorType] PRIMARY KEY CLUSTERED ([Id] ASC)
);



