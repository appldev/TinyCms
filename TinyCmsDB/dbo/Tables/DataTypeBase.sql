CREATE TABLE [dbo].[DataTypeBase] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [Name]             VARCHAR (30)     NOT NULL,
    [DisplayName]      VARCHAR (100)    NULL,
    [Model]            VARCHAR (MAX)    NULL,
    [RenderTypeId]     INT              CONSTRAINT [DF_DataType_RenderTypeId] DEFAULT ((0)) NOT NULL,
    [DataTypeScriptId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_DataTypeBase] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DataTypeBase_LibraryItemBase] FOREIGN KEY ([DataTypeScriptId]) REFERENCES [dbo].[LibraryItemBase] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK_DataTypeBase_RenderType] FOREIGN KEY ([RenderTypeId]) REFERENCES [dbo].[RenderType] ([Id])
);

