using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
   // private readonly IWorkflowController _workflowService;
        private readonly IPersistenceProvider _workflowStore;



        public ItemWorkflowController(ILogger<ItemWorkflowController> logger, IPersistenceProvider workflowStore //IWorkflowController workflowService, 
        )
        {
            //_workflowService = workflowService;
            _workflowStore = workflowStore;
            _logger = logger;

        }

      

    [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {

            var result = await _workflowStore.GetWorkflowInstance(id);
            _logger.LogInformation("this is hello - ----------");


            return Ok("hello" + id);
        }


    }
}
