CREATE TABLE [dbo].[DataTypeField] (
    [DataTypeId]  UNIQUEIDENTIFIER NOT NULL,
    [FieldBaseId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_DataTypeField] PRIMARY KEY CLUSTERED ([DataTypeId] ASC, [FieldBaseId] ASC),
    CONSTRAINT [FK_DataTypeField_DataTypeBase] FOREIGN KEY ([DataTypeId]) REFERENCES [dbo].[DataTypeBase] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_DataTypeField_FieldBase] FOREIGN KEY ([FieldBaseId]) REFERENCES [dbo].[FieldBase] ([Id]) ON DELETE CASCADE
);

