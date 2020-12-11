using System;
using System.Threading.Tasks;
using Serilog;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace item_workflow.Steps
{
    public class HazmatApproval : StepBody
    {

        public string Message { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Logger.Information("Running.....HazmatApproval");
            Task.Delay(5000).Wait();

            Log.Logger.Information("Message {Message}", Message);
            return ExecutionResult.Next();
        }
    }
    
     
}
