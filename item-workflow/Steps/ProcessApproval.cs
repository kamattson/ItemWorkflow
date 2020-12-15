using System;
using System.Threading.Tasks;
using item_workflow.Database;
using item_workflow.Model;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace item_workflow.Steps
{
    public class ProcessApproval : StepBody
    {
        public Approval approval { get; set; }
        private readonly ILogger<NewItem> _logger;
        private readonly ItemDbContext _itemDbContext;

        public ProcessApproval(ILogger<NewItem> logger, ItemDbContext itemDbContext)
        {
            _logger = logger;
            _itemDbContext = itemDbContext;
        }


        public override ExecutionResult Run(IStepExecutionContext context)
        {
            _logger.LogInformation("Running..." + approval.ApprovalTypeCode);
            Task.Delay(2000).Wait();

            return ExecutionResult.Next();
        }
    }
}
