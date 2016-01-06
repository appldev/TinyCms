CREATE TABLE [dbo].[PublishedPageBase] (
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
    [IsExternal]     BIT              CONSTRAINT [DF_PublishedPageBase_IsExternal] DEFAULT ((0)) NOT NULL,
    [CreatedOn]      DATETIME         CONSTRAINT [DF_PublishedPageBase_CreatedOn] DEFAULT (getdate()) NOT NULL,
    [ModifiedOn]     DATETIME         CONSTRAINT [DF_PublishedPageBase_ModifiedOn] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_PublishedPageBase] PRIMARY KEY CLUSTERED ([PageFolderId] ASC, [Name] ASC, [Culture] ASC),
    CONSTRAINT [FK_PublishedPageBase_PageAudience] FOREIGN KEY ([PageAudienceId]) REFERENCES [dbo].[PageAudience] ([Id]),
    CONSTRAINT [FK_PublishedPageBase_PageFolderBase] FOREIGN KEY ([PageTypeId]) REFERENCES [dbo].[PageFolderBase] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PublishedPageBase_PageSecurity] FOREIGN KEY ([PageSecurityId]) REFERENCES [dbo].[PageSecurity] ([Id]),
    CONSTRAINT [FK_PublishedPageBase_PageType] FOREIGN KEY ([PageTypeId], [Culture]) REFERENCES [dbo].[PageType] ([Id], [Culture]) ON UPDATE CASCADE
);



