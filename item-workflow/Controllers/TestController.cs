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
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly ItemDbContext _itemDbContext;

        public TestController(ILogger<TestController> logger, ItemDbContext itemDbContext)
        {
            _logger = logger;
            _itemDbContext = itemDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("this is hello - ----------");

            return Ok(await _itemDbContext.Item.ToListAsync());

            //return "hello";
        }



     

    }


}
