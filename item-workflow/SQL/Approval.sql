DROP TABLE Workflow_Approval
GO

CREATE TABLE Workflow_Approval
(
    ApprovalId [INT] NOT NULL IDENTITY PRIMARY KEY,
    ItemId [INT] NOT NULL REFERENCES Item (Id), 
    ApprovalStatus [NVARCHAR] (1) NOT NULL,
    ApproverUser [NVARCHAR] (50) NOT NULL,
    ApprovalTypeCode [NVARCHAR] (20),
    ApproverText [NVARCHAR] (100),
    ApprovalDate [Datetime] NOT NULL,
)

INSERT INTO Workflow_Approval
    ([ItemId], [ApprovalStatus], [ApproverUser], [ApprovalTypeCode], [ApproverText], [ApprovalDate])
VALUES
    ( 2, N'A' , N'user/bar', N'PCT', N'This is funny', CURRENT_TIMESTAMP),
    ( 3, N'A' , N'user/foo', N'MANUF', N'This is fun', CURRENT_TIMESTAMP)


SELECT COUNT(*) as ApprovalCount
FROM dbo.Workflow_Approval;

SELECT a.ApprovalId, a.itemId, a.ApprovalStatus, a.ApproverUser,
    a.ApprovalTypeCode, a.ApproverText, a.ApprovalDate
FROM dbo.Workflow_Approval as a
GO



select  i.Workflow_ID, i.ProductTitle, a.ApprovalTypeCode, a.ApprovalStatus from Item i, Workflow_Approval a
where a.ItemId = 3