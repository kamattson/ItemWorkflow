DROP TABLE Item
GO

CREATE TABLE Item
(
    ItemId INT NOT NULL PRIMARY KEY,
    Name [NVARCHAR] (50) NOT NULL,
    Vendor [NVARCHAR] (50) NOT NULL
)

INSERT INTO Item
    ([ItemId],[Name],[Vendor])
VALUES
    ( 1, N'Saw', N'Craftsman'),
    ( 2, N'Nailer', N'Dewalt'),
    ( 3, N'Drill', N'Milwaukee'),
    ( 4, N'Tape Measure', N'Stanley')
GO
-- Query the total count of employees
SELECT COUNT(*) as ItemCount
FROM dbo.Item;
-- Query all employee information
SELECT i.ItemId, i.Name, i.Vendor
FROM dbo.Item as i
GO