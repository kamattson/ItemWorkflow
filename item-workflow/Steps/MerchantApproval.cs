using System;
using Microsoft.Extensions.Logging;
using Serilog;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace item_workflow.Steps
{
    public class MerchantApproval : StepBody
    {

        public object Message { get; internal set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Logger.Information("Running.....MerchantApproval");
            return ExecutionResult.Next();
        }
    }
}
