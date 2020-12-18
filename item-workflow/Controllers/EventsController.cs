using System;
using System.Threading.Tasks;
using item_workflow.Model;
using Microsoft.AspNetCore.Mvc;
using WorkflowCore.Interface;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace item_workflow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : Controller
    {
        private readonly IWorkflowController _workflowService;
        private readonly ILogger<EventsController> _logger;
        private readonly IWorkflowHost _host;

        public EventsController(IWorkflowController workflowService, ILogger<EventsController> logger, IWorkflowHost host)
        {
            _workflowService = workflowService;
            _logger = logger;
            _host = host;
        }

        [HttpPost("{eventName}/{eventKey}")]
        public async Task<IActionResult> Post(string eventName, string eventKey, [FromBody] Approval approvalData)
        {
            _logger.LogInformation("Event: {eventName}, {eventKey}", eventName, eventKey);
            await _workflowService.PublishEvent(eventName, eventKey, approvalData.ApprovalStatus);
            return Ok();
        }

        [HttpGet("merchantapprove/{eventKey}/{user}/{option}", Name = nameof(ApproveMerchant))]
        public async Task<IActionResult> ApproveMerchant(string eventKey, string user, string option)
        {
            _logger.LogInformation("merchantapproval: {eventKey}, {user}, {approvalData}", eventKey, user, option);
            var openItems = _host.GetOpenUserActions(eventKey);
            string key = null;
            foreach (var item in openItems)
            {
                foreach (var opt in item.Options)
                {
                    _logger.LogInformation(" - " + opt.Key + " : " + opt.Value + ", ");
                }

                key = item.Key;
                string value = item.Options.Single(x => x.Value == option).Value;
                _logger.LogInformation("Choosing key:" + key + " value:" + value);

            }


            
            await _host.PublishUserAction(key, user, option);
            return Ok();
        }
    }
}