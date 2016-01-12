CREATE TABLE [dbo].[DataTypeTableRow] (
    [Id]              UNIQUEIDENTIFIER CONSTRAINT [DF_DataTypeTableRow_Id] DEFAULT (newid()) NOT NULL,
    [DataTypeTableId] UNIQUEIDENTIFIER NOT NULL,
    [Model]           VARCHAR (MAX)    NULL,
    CONSTRAINT [PK_DataTypeTableRow] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DataTypeTableRow_DataTypeTable] FOREIGN KEY ([DataTypeTableId]) REFERENCES [dbo].[DataTypeTable] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE CLUSTERED INDEX [IX_DataTypeTableRow]
    ON [dbo].[DataTypeTableRow]([DataTypeTableId] ASC);

