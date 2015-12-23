CREATE VIEW dbo.DataType
AS
SELECT        dbo.DataTypeBase.Id, dbo.DataTypeBase.Name, dbo.DataTypeBase.DisplayName, dbo.DataTypeBase.Model, dbo.DataTypeBase.RenderTypeId, dbo.RenderType.Name AS RenderTypeName
FROM            dbo.DataTypeBase INNER JOIN
                         dbo.RenderType ON dbo.DataTypeBase.RenderTypeId = dbo.RenderType.Id