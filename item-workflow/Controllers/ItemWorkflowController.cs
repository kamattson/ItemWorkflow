using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using item_workflow.Database;
using item_workflow.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;


namespace item_workflow.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemWorkflowController : ControllerBase
    {
        private readonly ILogger<ItemWorkflowController> _logger;
        private readonly IWorkflowController _workflowService;
        private readonly IPersistenceProvider _workflowStore;
        private readonly ItemDbContext _itemDbContext;


        public ItemWorkflowController(ILogger<ItemWorkflowController> logger,
                IPersistenceProvider workflowStore,
                IWorkflowController workflowService,
                ItemDbContext itemDbContext)
        {
            _workflowService = workflowService;
            _workflowStore = workflowStore;
            _logger = logger;
            _itemDbContext = itemDbContext;

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _workflowStore.GetWorkflowInstance(id);
            _logger.LogInformation("Workflow Instance: {result}", result);
            return Ok("Workflow Instance: {result}" + result);
        }

        [HttpPost("{workflowName}")]
        public IActionResult Post(string workflowName, [FromBody] Item itemData)
        {
            _logger.LogInformation("Starting Workflow: {workflowName}", workflowName);
            var workflowId = _workflowService.StartWorkflow(workflowName, itemData).Result;

            try
            {
                itemData.WorkflowId = Guid.Parse(workflowId);

            }
            catch (ArgumentNullException)
            {
                _logger.LogError("The string to be parsed is null.");
            }
            catch (FormatException)
            {
                _logger.LogError($"Bad format: {workflowId}");
            }

            // Maybe check for duplicates or already existing 
            //var item = _itemDbContext.Item
            //    .Single(i => i.ProductTitle == itemData.ProductTitle && i.Vendor == itemData.Vendor  );

            
            _itemDbContext.Item.Add(itemData);
            _itemDbContext.SaveChanges();

            return Ok("WorkflowId: " + workflowId);
        }

    }
}
