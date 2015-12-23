CREATE TABLE [dbo].[PageType] (
    [Id]           UNIQUEIDENTIFIER CONSTRAINT [DF__tmp_ms_xx_Pa__Id__173876EA] DEFAULT (newid()) NOT NULL,
    [Culture]      VARCHAR (10)     NOT NULL,
    [PageHostId]   UNIQUEIDENTIFIER NOT NULL,
    [PageLayoutId] UNIQUEIDENTIFIER NULL,
    [DataTypeId]   UNIQUEIDENTIFIER NULL,
    [Name]         VARCHAR (100)    NOT NULL,
    [Title]        VARCHAR (100)    NOT NULL,
    [Html]         VARCHAR (MAX)    NULL,
    CONSTRAINT [PK_PageType] PRIMARY KEY CLUSTERED ([Id] ASC, [Culture] ASC),
    CONSTRAINT [FK_PageType_DataTypeBase] FOREIGN KEY ([DataTypeId]) REFERENCES [dbo].[DataTypeBase] ([Id]),
    CONSTRAINT [FK_PageType_PageLayout] FOREIGN KEY ([PageLayoutId], [Culture], [PageHostId]) REFERENCES [dbo].[PageLayout] ([Id], [Culture], [PageHostId])
);




GO
EXECUTE sp_addextendedproperty @name = N'TinySql.DisplayName', @value = N'1033=Page Template
1030=Sideskabeloner', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PageType';

