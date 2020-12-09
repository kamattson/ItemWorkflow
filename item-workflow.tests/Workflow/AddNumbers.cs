using System;
using WorkflowCore.Models;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;

namespace item_workflow.tests.Workflow
{
    public class AddNumbers : StepBody
    {
        public int Input1 { get; set; }
        public int Input2 { get; set; }
        public int Output { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Output = (Input1 + Input2);
            return ExecutionResult.Next();

        }
    }
}
