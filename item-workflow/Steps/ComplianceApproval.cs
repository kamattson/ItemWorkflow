using System;
using Serilog;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace item_workflow.Steps
{
    public class ComplianceApproval : StepBody
    {

        public string Message { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Logger.Information("Running.....ComplianceApproval");
            Task.Delay(2000).Wait();

            Log.Logger.Information("Message {Message}", Message);
            return ExecutionResult.Next();
        }
    }


}
