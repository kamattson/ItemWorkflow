using System;
using FluentAssertions;
using item_workflow.Model;
using item_workflow.Workflows;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using WorkflowCore.Models;
using WorkflowCore.Testing;

namespace item_workflow.tests
{
    [TestFixture]
    public class ItemWorkflowTest : WorkflowTest<ItemWorkflow, Item>
    {

        [SetUp]
        protected override void Setup()
        {
            base.Setup();
        }

        [Test]
        public void NUnit_workflow_test_sample()
        {
            var workflowId = StartWorkflow(new Item() { Name = "foo",  Vendor = "skil" });
            WaitForWorkflowToComplete(workflowId, TimeSpan.FromSeconds(30));

            GetStatus(workflowId).Should().Be(WorkflowStatus.Complete);
            UnhandledStepErrors.Count.Should().Be(0);
            GetData(workflowId).Name.Should().Be("foo");
            
        }

    }
}
