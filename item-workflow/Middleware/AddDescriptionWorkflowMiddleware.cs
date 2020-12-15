using System;
using System.Threading.Tasks;
using item_workflow.Workflows;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace item_workflow.Middleware
{
    public class AddDescriptionWorkflowMiddleware : IWorkflowMiddleware
    {
        public WorkflowMiddlewarePhase Phase => WorkflowMiddlewarePhase.PreWorkflow;
        public Task HandleAsync(WorkflowInstance workflow, WorkflowDelegate next)
        {
            if (workflow.Data is IDescriptiveWorkflowParams descriptiveParams)
            {
                workflow.Description = descriptiveParams.Description;
            }

            return next();
        }
    }
}
