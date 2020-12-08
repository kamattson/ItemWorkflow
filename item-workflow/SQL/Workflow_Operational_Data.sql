
CREATE TABLE Workflow_Operational_Data
(
    Workflow_Id INT NOT NULL PRIMARY KEY,
    Article_Source_Flag [NVARCHAR] (1) NOT NULL,
    Last_Update_Date [Datetime] NOT NULL
)


INSERT INTO Workflow_Operational_Data
    ([Workflow_Id],[Article_Source_Flag],[Last_Update_Date])
VALUES
    ( 1, N'A', CURRENT_TIMESTAMP),
    ( 2, N'B', CURRENT_TIMESTAMP),
    ( 3, N'D', CURRENT_TIMESTAMP)
  
GO


SELECT COUNT(*) as ItemCount
FROM dbo.Workflow_Operational_Data;

SELECT i.Workflow_Id, i.Article_Source_Flag, i.Last_Update_Date
FROM dbo.Workflow_Operational_Data as i
GO