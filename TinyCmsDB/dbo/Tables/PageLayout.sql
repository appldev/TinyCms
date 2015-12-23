CREATE TABLE [dbo].[PageLayout] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [Culture]    VARCHAR (10)     NOT NULL,
    [PageHostId] UNIQUEIDENTIFIER NOT NULL,
    [Name]       VARCHAR (50)     NOT NULL,
    [Title]      VARCHAR (100)    NOT NULL,
    [Html]       VARCHAR (MAX)    NULL,
    CONSTRAINT [PK_PageLayout] PRIMARY KEY CLUSTERED ([Id] ASC, [Culture] ASC, [PageHostId] ASC)
);

