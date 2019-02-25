INSERT INTO [dbo].[ListCheckItemKind]
           ([ListCheckItemKindID]
           ,[Title])
SELECT
1,'Без выделения'
UNION ALL
SELECT
2,'Простой'
UNION ALL
SELECT
3,'Зачеркивание'

GO

INSERT INTO [dbo].[ListKind] ([ListKindID],[Title])  
SELECT
1,'Простой'
UNION ALL
SELECT
2,'Маркер'
UNION ALL
SELECT
3,'Нумерация'