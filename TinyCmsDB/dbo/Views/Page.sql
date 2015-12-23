CREATE VIEW [dbo].[Page]
AS
SELECT          p.Id,p.Name, p.Culture, ph.Id AS PageHostId, p.PageFolderId, p.PageTypeId, pt.DataTypeId, p.Title, p.Description, CASE p.PageTypeId WHEN NULL THEN p.Model ELSE pt.Html END AS Html, CASE p.PageTypeId WHEN NULL THEN NULL ELSE p.Model END AS Model, p.RequireSsl, p.PageSecurityId, p.PageAudienceId, folder.folderlevel as FolderLevel, '/' + p.Culture + folder.folderpath + p.Name AS FullPath, COALESCE(ph.ViewPath + '/templates/' + p.Culture + '/' + pt.Name, ph.ViewPath + '/' + p.Culture + folder.folderpath + p.Name + '.cshtml') AS FilePath
FROM            dbo.PageBase AS p 
INNER JOIN		dbo.PageFolder AS folder ON p.PageFolderId = folder.Id
INNER JOIN		dbo.PageHost AS ph ON folder.PageHostId = ph.Id AND ph.Culture = p.Culture
LEFT JOIN      dbo.PageType AS pt ON (p.PageTypeId = pt.Id AND p.Culture = pt.Culture)