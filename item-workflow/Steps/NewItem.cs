using System;
using item_workflow.Database;
using item_workflow.Model;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace item_workflow.Steps
{
    public class NewItem : StepBody
    {

        public Item item { get; set; }
        private readonly ILogger<NewItem> _logger;
        private readonly ItemDbContext _itemDbContext;

        public NewItem(ILogger<NewItem> logger, ItemDbContext itemDbContext)
        {
            _logger = logger;
            _itemDbContext = itemDbContext;
        }


        public override ExecutionResult Run(IStepExecutionContext context)
        {
            _logger.LogInformation("New Item added to workfow", item);
            item.WorkflowId = Guid.Parse(context.Workflow.Id);

            _itemDbContext.Item.Update(item);

            return ExecutionResult.Next();

        }
    }
}
