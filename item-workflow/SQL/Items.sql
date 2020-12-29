use testwfdb

DROP TABLE Workflow_Header
GO

CREATE TABLE Workflow_Header 
(
    NIO_request_ID [INT] NOT NULL  PRIMARY KEY ,
    Workflow_ID [UNIQUEIDENTIFIER] NOT NULL, 
    ProductTitle [NVARCHAR] (50) NOT NULL,
    Vendor [NVARCHAR] (50) NOT NULL, 
    ArticleSourceFlag [NVARCHAR] (1) NOT NULL,
    Price [INT] NOT NULL,
    HazardousFlag [NVARCHAR], 
    LastUpdateDate [Datetime] NOT NULL
)

DELETE FROM Workflow_Header;

INSERT INTO Workflow_Header
    ([Id], [Workflow_ID], [ProductTitle], [ArticleSourceFlag], [Vendor],  [Price], [HazardousFlag],  [LastUpdateDate])
VALUES
    ( 1, (select cast(0x0 as uniqueidentifier)), N'Saw', N'A', N'Craftsman', 0, '', CURRENT_TIMESTAMP),
    ( 2, (select cast(0x0 as uniqueidentifier)), N'Nailer', N'B', N'Dewalt', 0, '', CURRENT_TIMESTAMP),
    ( 3, (select cast(0x0 as uniqueidentifier)), N'Drill', N'D', N'Milwaukee', 0, '',CURRENT_TIMESTAMP),
    ( 4, (select cast(0x0 as uniqueidentifier)), N'Tape Measure', N'A', N'Stanley', 0,'', CURRENT_TIMESTAMP)
GO


SELECT COUNT(*) as ItemCount
FROM dbo.Workflow_Header;

SELECT i.id, i.Workflow_ID, i.ProductTitle, i.ArticleSourceFlag, i.Vendor, i.Price, i.HazardousFlag, i.LastUpdateDate
FROM dbo.Workflow_Header as i
GO