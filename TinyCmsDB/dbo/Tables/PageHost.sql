CREATE TABLE [dbo].[PageHost] (
    [Id]       UNIQUEIDENTIFIER CONSTRAINT [DF_PageHost_Id] DEFAULT (newid()) NOT NULL,
    [Name]     VARCHAR (50)     NULL,
    [Culture]  VARCHAR (10)     CONSTRAINT [DF_PageHost_LCID] DEFAULT ((1033)) NOT NULL,
    [ViewPath] VARCHAR (50)     NULL,
    CONSTRAINT [PK_PageHost] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_PageHost_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

