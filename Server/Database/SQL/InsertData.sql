INSERT INTO [dbo].[ListCheckItemKind]
           ([ListCheckItemKindID]
           ,[Title])
SELECT
1,'��� ���������'
UNION ALL
SELECT
2,'�������'
UNION ALL
SELECT
3,'������������'

GO

INSERT INTO [dbo].[ListKind] ([ListKindID],[Title])  
SELECT
1,'�������'
UNION ALL
SELECT
2,'������'
UNION ALL
SELECT
3,'���������'