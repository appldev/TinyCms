CREATE TABLE [dbo].[PublishedPage] (
    [Name]           VARCHAR (100)    NOT NULL,
    [Culture]        VARCHAR (10)     CONSTRAINT [DF_PublishedPage_LCID] DEFAULT ((1030)) NOT NULL,
    [PageFolderId]   UNIQUEIDENTIFIER NOT NULL,
    [Id]             UNIQUEIDENTIFIER CONSTRAINT [DF_PublishedPage_Id] DEFAULT (newid()) NOT NULL,
    [Title]          VARCHAR (100)    NULL,
    [Description]    VARCHAR (400)    NULL,
    [Model]          VARCHAR (MAX)    NULL,
    [PageTypeId]     UNIQUEIDENTIFIER NULL,
    [RequireSsl]     BIT              CONSTRAINT [DF_PublishedPage_RequireSsl] DEFAULT ((0)) NOT NULL,
    [PageSecurityId] INT              CONSTRAINT [DF_PublishedPage_PageSecurityId] DEFAULT ((0)) NOT NULL,
    [PageAudienceId] INT              NULL,
    CONSTRAINT [PK_PublishedPage] PRIMARY KEY CLUSTERED ([PageFolderId] ASC, [Name] ASC, [Culture] ASC),
    CONSTRAINT [FK_PublishedPage_PageAudience] FOREIGN KEY ([PageAudienceId]) REFERENCES [dbo].[PageAudience] ([Id]),
    CONSTRAINT [FK_PublishedPage_PageFolder] FOREIGN KEY ([PageTypeId]) REFERENCES [dbo].[PageFolder] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PublishedPage_PageSecurity] FOREIGN KEY ([PageSecurityId]) REFERENCES [dbo].[PageSecurity] ([Id]),
    CONSTRAINT [FK_PublishedPage_PageType] FOREIGN KEY ([PageTypeId], [Culture]) REFERENCES [dbo].[PageType] ([Id], [Culture]) ON UPDATE CASCADE
);

