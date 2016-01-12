CREATE TABLE [dbo].[ListItem] (
    [ListDefinitionId] UNIQUEIDENTIFIER NOT NULL,
    [Value]            VARCHAR (40)     NOT NULL,
    [Text]             VARCHAR (100)    NOT NULL,
    [Selected]         BIT              CONSTRAINT [DF_ListItem_Selected] DEFAULT ((0)) NOT NULL,
    [Disabled]         BIT              CONSTRAINT [DF_ListItem_Disabled] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ListItem] PRIMARY KEY CLUSTERED ([ListDefinitionId] ASC, [Value] ASC),
    CONSTRAINT [FK_ListItem_ListDefinition] FOREIGN KEY ([ListDefinitionId]) REFERENCES [dbo].[ListDefinition] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

