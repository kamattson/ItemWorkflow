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
                    .Input(step => step.CalcPrice, data => data.Price)
                    .Input(step => step.Vendor, data => data.Vendor)
                    .Output(data => data.Price, step => step.CalcPrice)
                .WaitFor("ApprovePCT", (data, context) => context.Workflow.Id, data => DateTime.Now)
                     .Output(data => data.ApprovalStatus, step => step.EventData)
                .Then<CustomMessage>()
                    .Input(step => step.Message, data => "The vendor from the event is " + data.Vendor);

            var branch2 = builder.CreateBranch()
                .StartWith<MerchantApproval>()
                .WaitFor("MerchantApprove", (data, context) => context.Workflow.Id, data => DateTime.Now)
                     .Output(data => data.ApprovalStatus, step => step.EventData)
                .Then<CustomMessage>()
                    .Input(step => step.Message, data => "-----Complete Merchant Approval Step ------")
                .Parallel()
                    .Do(then =>
                        then.StartWith<HazmatApproval>()
                            .Input(step => step.Message, data => "Item 1.1")
                            .WaitFor("HazmatApproval", (data, context) => context.Workflow.Id, data => DateTime.Now)
                                .Output(data => data.ApprovalStatus, step => step.EventData)
                            .Then<CustomMessage>()
                                .Input(step => step.Message, data => "-----Complete HazmatApproval Approval Step ------"))
                    .Do(then =>
                        then.StartWith<ComplianceApproval>()
                             .Input(step => step.Message, data => "Item 2.1")
                             .WaitFor("ComplianceApproval", (data, context) => context.Workflow.Id, data => DateTime.Now)
                                .Output(data => data.ApprovalStatus, step => step.EventData)
                             .Then<CustomMessage>()
                                .Input(step => step.Message, data => "-----Complete ComplianceApproval Approval Step ------"))
                .Join()
                    .Then<PricingAutoAssign>()
                        .Input(step => step.CalcPrice, data => data.Price)
                        .Input(step => step.Vendor, data => data.Vendor)
                        .Output(data => data.Price, step => step.CalcPrice);

            builder
                .StartWith(context => ExecutionResult.Next())
                .Decide(data => data.ArticleSourceFlag)
                    .Branch((data, outcome) => data.ArticleSourceFlag == "A", branch1)
                    .Branch((data, outcome) => data.ArticleSourceFlag == "B", branch2)
                    .Branch((data, outcome) => data.ArticleSourceFlag == "D", branch2);

        }

    }


}
