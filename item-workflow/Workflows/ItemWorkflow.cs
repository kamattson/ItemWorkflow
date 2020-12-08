using System;
using item_workflow.Model;
using item_workflow.Steps;
using Microsoft.Extensions.Logging;
using Serilog;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace item_workflow.Workflows
{
    public class ItemWorkflow : IWorkflow<Item>
    {

        public string Id => "ItemWorkflow";

        public int Version => 1;

        public void Build(IWorkflowBuilder<Item> builder)
        {

            Log.Information("[Workflow] Build: {Id}", Id);

            var branch1 = builder.CreateBranch()
                .StartWith<PricingAutoAssign>()
                    .Input(step => step.Message, data => "--- Start PricingAutoAssign Step-------")
                .WaitFor("ApprovePCT", data => "0")
                     .Output(data => data.Name, step => step.EventData)
                .Then<CustomMessage>()
                    .Input(step => step.Message, data => "The data from the event is " + data.Name);

            var branch2 = builder.CreateBranch()
                .StartWith<MerchantApproval>()
                    .Input(step => step.Message, data => "---- Start Merchant Approval Step-------")
                .Then<CustomMessage>()
                    .Input(step => step.Message, data => "-----Complete Merchant Approval Step ------");

            builder
                .StartWith(context => ExecutionResult.Next())

                .Decide(data => data.ArticleSourceFlag)
                    .Branch((data, outcome) => data.ArticleSourceFlag == "A", branch1)
                    .Branch((data, outcome) => data.ArticleSourceFlag == "B", branch2)
                    .Branch((data, outcome) => data.ArticleSourceFlag == "D", branch2);

        }

    }

    
}
