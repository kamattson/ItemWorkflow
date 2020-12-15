using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using item_workflow.Database;
using item_workflow.Model;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace item_workflow.Steps
{
    public class PricingAutoAssign : StepBody
    {

        private readonly ILogger<PricingAutoAssign> _logger;
        private readonly ItemDbContext _itemDbContext;

        public Item item { get; set; }

        public int StepPrice { get; set; }
        //public string Vendor { get; set; }
        public int CalcPrice { get; set; }

        public PricingAutoAssign(ILogger<PricingAutoAssign> logger, ItemDbContext itemDbContext)
        {
            _logger = logger;
            _itemDbContext = itemDbContext;
        }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            _logger.LogInformation("Running.... Pricing Autoassign for Vendor " + item.ToString());
            Task.Delay(2000).Wait();

            //Do price lookup here
            Random random = new Random();
            int randomNumber = random.Next(0, 1000);
            CalcPrice = (StepPrice + randomNumber);
            _logger.LogInformation("New Item Price {CalcPrice}", CalcPrice);

            var dbitem = _itemDbContext.Item.Where(i => i.WorkflowId == item.WorkflowId).Single();
            dbitem.Price = CalcPrice;
            _itemDbContext.Item.Update(dbitem);
            _itemDbContext.SaveChanges();

            return ExecutionResult.Next();
        }
    }
}