using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using item_workflow.Database;
using item_workflow.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;


namespace item_workflow.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly ItemDbContext _itemDbContext;

        public ItemController(ILogger<ItemController> logger, ItemDbContext itemDbContext)
        {
            _logger = logger;
            _itemDbContext = itemDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("In TestController.Get(), Looking up Items....");
            return Ok(await _itemDbContext.Item.Include(a => a.Approvals).ToListAsync());
        }

    }


}
