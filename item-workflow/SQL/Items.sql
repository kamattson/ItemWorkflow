DROP TABLE Item
GO

CREATE TABLE Item
(
    ItemId INT NOT NULL PRIMARY KEY,
    Name [NVARCHAR] (50) NOT NULL,
    ArticleSourceFlag [NVARCHAR] (1) NOT NULL,
    Vendor [NVARCHAR] (50) NOT NULL, 
    LastUpdateDate [Datetime]
NOT NULL
)

INSERT INTO Item
    ([ItemId],[Name], [ArticleSourceFlag], [Vendor], [LastUpdateDate])
VALUES
    ( 1, N'Saw', N'A', N'Craftsman', CURRENT_TIMESTAMP),
    ( 2, N'Nailer', N'B', N'Dewalt', CURRENT_TIMESTAMP),
    ( 3, N'Drill', N'D', N'Milwaukee', CURRENT_TIMESTAMP),
    ( 4, N'Tape Measure', N'A', N'Stanley', CURRENT_TIMESTAMP)
GO

SELECT COUNT(*) as ItemCount
FROM dbo.Item;

SELECT i.ItemId, i.Name, i.ArticleSourceFlag, i.Vendor, i.LastUpdateDate
FROM dbo.Item as i
GO