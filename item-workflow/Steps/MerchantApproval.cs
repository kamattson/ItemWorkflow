using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace item_workflow.Steps
{
    public class MerchantApproval : StepBody
    {

        public string Message { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Logger.Information("Running.....MerchantApproval");
            Task.Delay(2000).Wait();

            Log.Logger.Information("Message {Message}", Message);
            return ExecutionResult.Next();
        }
    }
}
