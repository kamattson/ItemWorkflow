using System;
using item_workflow.Model;
using item_workflow.Steps;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace item_workflow.Workflows
{
    public class ItemWorkflow : IWorkflow<Item>
    {

        private readonly ILogger<ItemWorkflow> _logger;

        public ItemWorkflow(ILogger<ItemWorkflow> logger)
        {
            _logger = logger;
        }
    

        public string Id => "ItemWorkflow";

        public int Version => 1;

        public void Build(IWorkflowBuilder<Item> builder)
        {
            _logger.LogInformation("------ In workflow build ---------- ");

            builder
                .StartWith(context => ExecutionResult.Next())
                .Then<PricingAutoAssign>()
                .WaitFor("ApprovePCT", data => "0")
                     .Output(data => data.Name, step => step.EventData)
                .Then<CustomMessage>() 
                    .Input(step => step.Message, data => "The data from the event is " + data.Name)
                .Then(context => _logger.LogInformation("workflow complete"));
        }

    }

    
}
