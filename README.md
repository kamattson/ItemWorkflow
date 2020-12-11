# ItemWorkflow

Uses WorkflowCore library to build workflows.
[workflowcore](https://workflow-core.readthedocs.io/en/latest/) 


## Installation

Use the in memory db or one of the providers used by workflow core.
Create testwfdb database for extra data.

Use Microsoft's [Secret Manager](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=linux ) tool to hide db passwords locally.


```bash
dotnet user-secrets init
dotnet user-secrets set "TestDB" "your super secret password"
dotnet user-secrets set "WorkflowDB" "your super secret password"
dotnet user-secrets list
```

## Usage

```c#
            var builder = new SqlConnectionStringBuilder(
                Configuration.GetConnectionString("TestDB"));
                builder.Password = Configuration["WFPass"];
            _connection = builder.ConnectionString;
```

d



