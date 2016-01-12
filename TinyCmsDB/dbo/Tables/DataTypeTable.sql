CREATE TABLE [dbo].[DataTypeTable] (
    [Id]         UNIQUEIDENTIFIER CONSTRAINT [DF_DataTypeTable_Id] DEFAULT (newid()) NOT NULL,
    [Name]       VARCHAR (200)    NOT NULL,
    [DataTypeId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_DataTypeTable] PRIMARY KEY CLUSTERED ([Id] ASC)
);

