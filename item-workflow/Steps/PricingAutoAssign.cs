using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using item_workflow.Model;
using Microsoft.Extensions.Logging;
using Serilog;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace item_workflow.Steps
{
    public class PricingAutoAssign : StepBody
    {
        public int StepPrice { get; set; }
        public string Vendor { get; set; }
        public int CalcPrice { get; set; }


        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Logger.Information("Running.... Pricing Autoassign for Vendor " + Vendor);
            Task.Delay(2000).Wait();

            Random random = new Random();
            int randomNumber = random.Next(0, 1000);

            //do something with Vendor....then calc price

            CalcPrice = (StepPrice + randomNumber);

            Log.Logger.Information("New Item Price {CalcPrice}", CalcPrice);


            // Call Autoassign API
            return ExecutionResult.Next();
        }
    }
}