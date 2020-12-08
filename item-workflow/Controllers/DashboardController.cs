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

        //[HttpGet]
        //public async Task<IActionResult> Get(WorkflowStatus? status, string type, DateTime? createdFrom, DateTime? createdTo, int skip, int take)
        //{
            // this method is obsolete.  Need to obtain the data ourselves
            //var result = await _workflowStore.GetWorkflowInstances(status, type, createdFrom, createdTo, skip, take);
            //return Json(result.ToList());
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _workflowStore.GetWorkflowInstance(id);
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