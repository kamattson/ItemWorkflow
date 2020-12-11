using System;
using System.Threading.Tasks;
using item_workflow.Model;
using Microsoft.AspNetCore.Mvc;
using WorkflowCore.Interface;
using Microsoft.Extensions.Logging;

namespace item_workflow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : Controller
    {
        private readonly IWorkflowController _workflowService;
        private readonly ILogger<EventsController> _logger;

        public EventsController(IWorkflowController workflowService, ILogger<EventsController> logger )
        {
            _workflowService = workflowService;
            _logger = logger;
        }

        [HttpPost("{eventName}/{eventKey}")]
        public async Task<IActionResult> Post(string eventName, string eventKey, [FromBody] Item itemData)
        {
            _logger.LogInformation("Event: {eventName}, {eventKey}", eventName, eventKey);
            await _workflowService.PublishEvent(eventName, eventKey, itemData.ApprovalStatus);
            return Ok();
        }

    }
}