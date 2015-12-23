CREATE TABLE [dbo].[FieldBase] (
    [Id]                   INT              NOT NULL,
    [Name]                 VARCHAR (30)     NOT NULL,
    [DisplayName]          VARCHAR (200)    NULL,
    [Required]             BIT              CONSTRAINT [DF_ContentType_Required] DEFAULT ((0)) NOT NULL,
    [UseValidation]        BIT              CONSTRAINT [DF_ContentType_UseValidation] DEFAULT ((0)) NOT NULL,
    [ValidationMessage]    VARCHAR (400)    NULL,
    [ValidationAttributes] VARCHAR (400)    NULL,
    [ValidationMin]        INT              CONSTRAINT [DF_ContentType_ValidationMin] DEFAULT ((0)) NOT NULL,
    [ValidationMax]        INT              CONSTRAINT [DF_ContentType_ValidationMax] DEFAULT ((0)) NOT NULL,
    [MissingValue]         VARCHAR (MAX)    NULL,
    [DefaultValue]         VARCHAR (MAX)    NULL,
    [PlaceHolder]          VARCHAR (400)    NULL,
    [HelpText]             VARCHAR (400)    NULL,
    [FieldTypeId]          INT              NOT NULL,
    [EditorTypeId]         INT              NOT NULL,
    [DataTypeId]           UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_FieldBase] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FieldBase_DataTypeBase] FOREIGN KEY ([DataTypeId]) REFERENCES [dbo].[DataTypeBase] ([Id]),
    CONSTRAINT [FK_FieldBase_EditorType] FOREIGN KEY ([EditorTypeId]) REFERENCES [dbo].[EditorType] ([Id]),
    CONSTRAINT [FK_FieldBase_FieldType] FOREIGN KEY ([FieldTypeId]) REFERENCES [dbo].[FieldType] ([Id])
);

