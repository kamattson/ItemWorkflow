using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace item_workflow.Steps
{
    public class PricingAutoAssign : StepBody
    {
        private string message = "Hello Pricing Autoassign";


        public string Message { get => message; set => message = value; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Logger.Information("Running Pricing Autoassign");

            // Call Autoassign API

            return ExecutionResult.Next();
        }
    }
}