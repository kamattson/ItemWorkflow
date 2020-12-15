use WorkflowCore

use testwfdb

SELECT *
FROM
    INFORMATION_SCHEMA.TABLES;
GO

SELECT * FROM wfc.Event;
SELECT * FROM wfc.Workflow;
SELECT * FROM wfc.Subscription;
SELECT * FROM wfc.ExecutionPointer;
