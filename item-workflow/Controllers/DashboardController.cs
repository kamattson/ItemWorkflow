using System;
using System.Threading.Tasks;
using item_workflow.Model;
using Microsoft.AspNetCore.Mvc;
using WorkflowCore.Interface;
using Microsoft.Extensions.Logging;
using WorkflowCore.Models;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using WorkflowCore.Users.Models;

namespace item_workflow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IWorkflowHost _workflowHost;
        private readonly IPersistenceProvider _workflowStore;
        private readonly IWorkflowRegistry _registry;


        public DashboardController(ILogger<DashboardController> logger, IWorkflowRegistry registry,
        IPersistenceProvider workflowStore, IWorkflowHost workflowHost)
        {
            _workflowHost = workflowHost;
            _workflowStore = workflowStore;
            _registry = registry;
            _logger = logger;

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _workflowStore.GetWorkflowInstance(id);
            return Json(result);
        }

        [HttpGet("openuseractions/{workflowId}", Name = nameof(GetOpenUserActions))]
        public IActionResult GetOpenUserActions(string workflowId)
        {
            var openItems = _workflowHost.GetOpenUserActions(workflowId);
            return Ok(openItems);
        }

        [HttpGet("events/{workflowId}", Name = nameof(GetOpenEvents))]
        public async Task<IActionResult> GetOpenEvents(string workflowId)
        {
            var workflow = await _workflowStore.GetWorkflowInstance(workflowId);
            List<string> result = new List<string>();

            var pointers = workflow.ExecutionPointers.Where(x => !x.EventPublished);
            foreach (var pointer in pointers)
            {

                _logger.LogInformation("Event:" + pointer.EventName);
                //var item = new OpenEvenAction()
                //{
                //    Key = pointer.EventKey,
                //    Prompt = Convert.ToString(pointer.ExtensionAttributes[UserTask.ExtPrompt]),
                //    AssignedPrincipal = Convert.ToString(pointer.ExtensionAttributes[UserTask.ExtAssignPrincipal]),
                //    Options = (pointer.ExtensionAttributes[UserTask.ExtUserOptions] as Dictionary<string, string>)
                //};

                result.Add(pointer.EventName);
            }

            return Json(result);
        }


        [HttpPost("{id}")]
        [HttpPost("{id}/{version}")]
        public async Task<IActionResult> Post(string id, int? version, string reference, [FromBody] JObject data)
        {
            string workflowId = null;
            var def = _registry.GetDefinition(id, version);
            if (def == null)
                return BadRequest(String.Format("Workflow defintion {0} for version {1} not found", id, version));
            if ((data != null) && (def.DataType != null))
            {
                var dataStr = JsonConvert.SerializeObject(data);
                var dataObj = JsonConvert.DeserializeObject(dataStr, def.DataType);
                workflowId = await _workflowHost.StartWorkflow(id, version, dataObj, reference);
            }
            else
            {
                workflowId = await _workflowHost.StartWorkflow(id, version, null, reference);
            }

            return Ok(workflowId);
        }

        [HttpPut("{id}/suspend")]
        public Task<bool> Suspend(string id)
        {
            return _workflowHost.SuspendWorkflow(id);
        }

        [HttpPut("{id}/resume")]
        public Task<bool> Resume(string id)
        {
            return _workflowHost.ResumeWorkflow(id);
        }

        [HttpDelete("{id}")]
        public Task<bool> Terminate(string id)
        {
            return _workflowHost.TerminateWorkflow(id);
        }


    }

}