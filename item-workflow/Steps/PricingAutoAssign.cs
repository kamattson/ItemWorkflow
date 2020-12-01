using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace item_workflow.Steps
{
    public class PricingAutoAssign : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Hello world");

            // Call the Autoassign API

            return ExecutionResult.Next();
        }
    }
}