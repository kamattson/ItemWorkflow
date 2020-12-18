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

            // Workflow branch executes DropShip items
            var dropshipBranch = builder.CreateBranch()
                .StartWith<NotifyApprovers>()
                .WaitFor("ApprovePCT", (data, context) => context.Workflow.Id, data => DateTime.Now)
                .Then<PricingAutoAssign>()
                    .Input(step => step.CalcPrice, data => data.Price)
                    .Input(step => step.item, data => data)
                    .Output(data => data.Price, step => step.CalcPrice)
                .Then<CustomMessage>()
                    .Input(step => step.Message, data => "The vendor from the event is " + data.Vendor);

            // Workflow branch executes RSC items and 
            var rscBranch = builder.CreateBranch()
                .StartWith<MerchantApproval>()
                .Then(context => Log.Logger.Information("---------- Start Merchant Approval Task ------------"))
                    .UserTask("MerchantApprove", data => "username")
                            .WithOption("Approved", "Approved").Do(then => then
                                .StartWith(context => Log.Logger.Information("You approved"))
                            )
                            .WithOption("Denied", "Denied").Do(then => then
                                .StartWith(context => Log.Logger.Information("You did not approve"))
                            )
                     .Then<CustomMessage>()
                    .Input(step => step.Message, data => "-----Complete Merchant Approval Step ------")

                    //.WaitFor("MerchantApprove", (data, context) => context.Workflow.Id, data => DateTime.Now)
                    //.If(data => data.isDSCert(false)).Do(then => then
                    // .StartWith(<CheckVendorDSCert>())
                    // .If(data.CertFlag.boolean(false)).Do(then => then
                    // .StartWith(<RejectWorkflow>()
                    // .EndWorkflow()
                    //.If(data => data.isDSCert(true)).Do(then => then
                    //    .StartWith(<RemoveRSCSelections>())
                    // .Attach(dropshipBranch);

                .Parallel()
                    .Do(then =>
                        then.StartWith<HazmatApproval>()
                            .Input(step => step.Message, data => "Item 1.1")
                            .If(data => data.HazardousFlag.Equals("Y")).Do(then => then
                                .WaitFor("HazmatApproval", (data, context) => context.Workflow.Id, data => DateTime.Now))
                            .Then<CustomMessage>()
                                .Input(step => step.Message, data => "-----Complete HazmatApproval Approval Step ------"))
                    .Do(then =>
                        then.StartWith<ComplianceApproval>()
                             .Input(step => step.Message, data => "Item 2.1")
                             .WaitFor("ComplianceApproval", (data, context) => context.Workflow.Id, data => DateTime.Now)
                             .Then<CustomMessage>()
                                .Input(step => step.Message, data => "-----Complete ComplianceApproval Approval Step ------"))
                .Join()
                    .Then<PricingAutoAssign>()
                        .Input(step => step.CalcPrice, data => data.Price)
                        .Input(step => step.item, data => data)
                        .Output(data => data.Price, step => step.CalcPrice);

            builder
                .StartWith<NewItem>()
                    // Associate the WorkflowID with the item in workflow database
                    .Input(step => step.item, data => data)
                    // This will set the item data in the workflow process so we can refer to it later.
                    .Output(data => data.WorkflowId, step => step.item.WorkflowId)


                .Decide(data => data.ArticleSourceFlag)
                    .Branch((data, outcome) => data.ArticleSourceFlag == "A", dropshipBranch)
                    .Branch((data, outcome) => data.ArticleSourceFlag == "B", rscBranch)
                    .Branch((data, outcome) => data.ArticleSourceFlag == "D", rscBranch);

        }

    }


}
